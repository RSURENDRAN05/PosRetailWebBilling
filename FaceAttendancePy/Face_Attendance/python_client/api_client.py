# ============================================================
# API Client v2
# Matches existing POS database schema:
#   pos_employeeinfo  → emp_id, emp_printname, emp_compid, emp_locid
#   employee_attendance → morning_in, evening_out, att_source
#   employee_face_encodings → new face table
# ============================================================

import requests
from config import ENDPOINTS, API_KEY, SESSION


class APIClient:
    """Handles all REST API calls to the PHP cloud backend."""

    TIMEOUT = 15

    @classmethod
    def _headers(cls) -> dict:
        headers = {
            "Content-Type": "application/json",
            "X-API-Key":    API_KEY,
        }
        if SESSION.get("token"):
            headers["Authorization"] = f"Bearer {SESSION['token']}"
        return headers

    @classmethod
    def _request(cls, method: str, url: str, params: dict = None, body: dict = None) -> dict:
        try:
            r = requests.request(
                method, url,
                headers=cls._headers(),
                params=params,
                json=body,
                timeout=cls.TIMEOUT,
            )
            try:
                return r.json()
            except ValueError:
                snippet = (r.text or "").strip().replace("\n", " ")[:180]
                if r.status_code >= 400:
                    return {
                        "success": False,
                        "message": f"Server error {r.status_code}. Non-JSON response: {snippet or '<empty body>'}",
                    }
                return {
                    "success": False,
                    "message": f"Invalid JSON response from server: {snippet or '<empty body>'}",
                }
        except requests.exceptions.ConnectionError:
            return {"success": False, "message": "Cannot connect to server. Check API_BASE_URL in config.py"}
        except requests.exceptions.Timeout:
            return {"success": False, "message": "Server request timed out"}
        except Exception as e:
            return {"success": False, "message": f"Request error: {str(e)}"}

    # ======================================================
    # AUTH
    # ======================================================

    @classmethod
    def admin_login(cls, username: str, password: str) -> dict:
        return cls._request("POST", ENDPOINTS["auth"], body={
            "action":   "admin_login",
            "username": username,
            "password": password,
        })

    @classmethod
    def get_admin_users(cls, com_id: str = None, loc_id: str = None) -> dict:
        params = {"action": "admin_users"}
        if com_id:
            params["com_id"] = com_id
        if loc_id:
            params["loc_id"] = loc_id
        return cls._request("GET", ENDPOINTS["auth"], params=params)

    @classmethod
    def pin_login(cls, loc_id: str, pin: str) -> dict:
        return cls._request("POST", ENDPOINTS["auth"], body={
            "action": "pin_login",
            "loc_id": loc_id,
            "pin":    pin,
        })

    @classmethod
    def test_connection(cls) -> tuple:
        r = cls.get_stats()
        if r.get("success"):
            return True, "Connected"
        return False, r.get("message", "Unknown error")

    # ======================================================
    # EMPLOYEES — reads from pos_employeeinfo
    # Returns: emp_id, emp_printname, emp_compid, emp_locid, etc.
    # ======================================================

    @classmethod
    def get_employees(cls, com_id: str = None, loc_id: str = None,
                      search: str = None, active: int = 1,
                      status: str = None) -> dict:
        if status is not None:
            params = {"status": status}
        else:
            params = {"status": "active" if active else "inactive"}
        if com_id:  params["com_id"] = com_id
        if loc_id:  params["loc_id"] = loc_id
        if search:  params["search"] = search
        return cls._request("GET", ENDPOINTS["employees"], params=params)

    @classmethod
    def get_employee(cls, emp_id: str) -> dict:
        return cls._request("GET", ENDPOINTS["employees"], params={"id": emp_id})

    # ======================================================
    # FACE ENCODINGS — reads/writes employee_face_encodings
    # Field name: emp_id  (NOT employee_id)
    # ======================================================

    @classmethod
    def save_face_encodings(cls, emp_id: str, encodings: list,
                             com_id: str = None, loc_id: str = None,
                             replace: bool = True) -> dict:
        return cls._request("POST", ENDPOINTS["faces"], body={
            "emp_id":     emp_id,           # matches employee_face_encodings.emp_id
            "com_id":     com_id or SESSION.get("com_id", ""),
            "loc_id":     loc_id or SESSION.get("loc_id", ""),
            "encodings":  encodings,
            "replace":    replace,
            "face_name":  "face",           # mirrors finger_name in employee_fingerprints
            "facetype":   "full",           # mirrors fingertype in employee_fingerprints
        })

    @classmethod
    def get_all_face_encodings(cls, com_id: str = None, loc_id: str = None) -> dict:
        params = {}
        if com_id:  params["com_id"] = com_id
        if loc_id:  params["loc_id"] = loc_id
        return cls._request("GET", ENDPOINTS["faces"], params=params)

    @classmethod
    def get_employee_face_encodings(cls, emp_id: str) -> dict:
        return cls._request("GET", ENDPOINTS["faces"], params={"id": emp_id})

    @classmethod
    def delete_face_encodings(cls, emp_id: str) -> dict:
        return cls._request("DELETE", ENDPOINTS["faces"], params={"id": emp_id})

    # ======================================================
    # ATTENDANCE — writes to employee_attendance
    # morning_in = check-in  |  evening_out = check-out
    # att_source = face/fingerprint/web/mobile/manual
    # ======================================================

    @classmethod
    def check_in(cls, emp_id: str, com_id: str = None, loc_id: str = None,
                 liveness_pass: int = 1, latitude: float = None,
                 longitude: float = None, gps_accuracy: float = None,
                 source: str = "face") -> dict:
        body = {
            "emp_id":       emp_id,
            "action":       "checkin",
            "source":       source,         # stored in att_source
            "method":       "face_scan",    # stored in att_method
            "com_id":       com_id or SESSION.get("com_id", ""),
            "loc_id":       loc_id or SESSION.get("loc_id", ""),
            "liveness_pass":liveness_pass,
        }
        if latitude  is not None: body["latitude"]  = latitude
        if longitude is not None: body["longitude"] = longitude
        if gps_accuracy is not None: body["gps_accuracy"] = gps_accuracy
        return cls._request("POST", ENDPOINTS["attendance"], body=body)

    @classmethod
    def check_out(cls, emp_id: str, com_id: str = None, loc_id: str = None,
                  source: str = "face", latitude: float = None,
                  longitude: float = None, gps_accuracy: float = None) -> dict:
        body = {
            "emp_id":   emp_id,
            "action":   "checkout",
            "source":   source,
            "com_id":   com_id or SESSION.get("com_id", ""),
            "loc_id":   loc_id or SESSION.get("loc_id", ""),
            "method":   "face_scan",
            "liveness_pass": 1,
        }
        if latitude is not None:
            body["latitude"] = latitude
        if longitude is not None:
            body["longitude"] = longitude
        if gps_accuracy is not None:
            body["gps_accuracy"] = gps_accuracy
        return cls._request("POST", ENDPOINTS["attendance"], body=body)

    @classmethod
    def get_attendance(cls, date: str = None, emp_id: str = None,
                       from_date: str = None, to_date: str = None,
                       com_id: str = None, loc_id: str = None,
                       all_records: bool = False) -> dict:
        params = {}
        if date:        params["date"] = date
        if emp_id:      params["emp"]  = emp_id
        if from_date:   params["from"] = from_date
        if to_date:     params["to"]   = to_date
        if all_records: params["all"]  = "1"
        if com_id:      params["com_id"] = com_id
        if loc_id:      params["loc_id"] = loc_id
        return cls._request("GET", ENDPOINTS["attendance"], params=params)

    # ======================================================
    # STATS — dashboard summary
    # ======================================================

    @classmethod
    def get_stats(cls, com_id: str = None, loc_id: str = None) -> dict:
        params = {}
        if com_id: params["com_id"] = com_id
        if loc_id: params["loc_id"] = loc_id
        return cls._request("GET", ENDPOINTS["stats"], params=params)

    # ======================================================
    # LOCATIONS (for login dropdown)
    # ======================================================

    @classmethod
    def get_locations(cls, branch_id: str = None) -> dict:
        params = {}
        if branch_id: params["branch"] = branch_id
        return cls._request("GET", ENDPOINTS["locations"], params=params)

    @classmethod
    def get_branches(cls, com_id: str = None) -> dict:
        params = {}
        if com_id: params["com_id"] = com_id
        return cls._request("GET", ENDPOINTS["branches"], params=params)
