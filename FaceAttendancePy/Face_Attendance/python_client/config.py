# ============================================================
# Face Attendance System v2 - Python Client Configuration
# Database: myposqrc_posretail
# ============================================================

# --- Cloud API Settings -------------------------------------
API_BASE_URL = "https://yourdomain.com/face_attendance/api"
API_KEY      = "face_attendance_secret_2024"

# --- Database Table Names (existing POS system) -------------
# DO NOT change these — they match your existing database
DB_TABLES = {
    "employees":   "pos_employeeinfo",          # existing employee table
    "fingerprints":"employee_fingerprints",     # existing fingerprint blob table
    "face_encodings":"employee_face_encodings", # NEW — created by integrate_existing_db.sql
    "attendance":  "employee_attendance",       # existing attendance table
}

# --- Column Mapping: pos_employeeinfo ----------------------
EMP_COLUMNS = {
    "id":          "emp_id",
    "first_name":  "emp_firstname",
    "last_name":   "emp_lastname",
    "display_name":"emp_printname",       # use this for display
    "designation": "emp_designation",
    "com_id":      "emp_compid",          # maps to com_id
    "loc_id":      "emp_locid",           # maps to loc_id
    "phone":       "emp_contactno",
    "active":      "emp_active",          # 1=active, 0=inactive
    "image":       "emp_image",
    "join_date":   "emp_joindate",
}

# --- Column Mapping: employee_attendance --------------------
ATT_COLUMNS = {
    "id":          "att_id",
    "emp_id":      "emp_id",
    "com_id":      "com_id",
    "loc_id":      "loc_id",
    "date":        "att_date",
    "check_in":    "morning_in",          # face check-in → morning_in
    "break_out":   "morning_out",         # optional break
    "break_in":    "break_in",            # optional break return
    "check_out":   "evening_out",         # face check-out → evening_out
    "work_hours":  "total_work_hours",
    "source":      "att_source",          # face/fingerprint/web/mobile/manual
    "method":      "att_method",
    "liveness":    "liveness_pass",
}

# --- Tenant Settings (set after login) ----------------------
DEFAULT_COM_ID    = "COM001"
DEFAULT_BRANCH_ID = ""
DEFAULT_LOC_ID    = ""

# --- API Endpoints ------------------------------------------
ENDPOINTS = {
    "auth":       f"{API_BASE_URL}/auth.php",
    "employees":  f"{API_BASE_URL}/employees.php",
    "faces":      f"{API_BASE_URL}/faces.php",
    "attendance": f"{API_BASE_URL}/attendance.php",
    "stats":      f"{API_BASE_URL}/stats.php",
    "branches":   f"{API_BASE_URL}/branches.php",
    "locations":  f"{API_BASE_URL}/locations.php",
}

# --- Face Recognition Settings ------------------------------
FACE_TOLERANCE       = 0.45
FACE_SAMPLES         = 5
FACE_CAPTURE_DELAY   = 0.3
MIN_FACE_CONFIDENCE  = 0.5

# --- Liveness Detection ------------------------------------
LIVENESS_ENABLED      = True
LIVENESS_BLINK_COUNT  = 2
LIVENESS_HEAD_TURN    = True
LIVENESS_TIMEOUT_SECS = 15

# --- Camera Settings ----------------------------------------
CAMERA_INDEX   = 0
FRAME_WIDTH    = 640
FRAME_HEIGHT   = 480
PREVIEW_WIDTH  = 480
PREVIEW_HEIGHT = 360

# --- Attendance Settings ------------------------------------
WORK_START_HOUR   = 9
WORK_START_MINUTE = 0
COOLDOWN_SECONDS  = 5
CHECK_OUT_MODE    = False

# --- GPS Settings -------------------------------------------
GPS_ENABLED          = True
GPS_GEOFENCE_ENABLED = False
GPS_OFFICE_LAT       = 3.1390
GPS_OFFICE_LNG       = 101.6869
GPS_GEOFENCE_RADIUS  = 200

# --- UI Settings --------------------------------------------
APP_TITLE      = "Face Attendance System"
APP_VERSION    = "2.0.0"
THEME_COLOR    = "#2C3E50"
ACCENT_COLOR   = "#27AE60"
DANGER_COLOR   = "#E74C3C"
WARNING_COLOR  = "#F39C12"
BG_COLOR       = "#ECF0F1"
CARD_COLOR     = "#FFFFFF"
TEXT_COLOR     = "#2C3E50"
MUTED_COLOR    = "#95A5A6"

FONT_FAMILY    = "Segoe UI"
FONT_LARGE     = ("Segoe UI", 18, "bold")
FONT_MEDIUM    = ("Segoe UI", 13, "bold")
FONT_NORMAL    = ("Segoe UI", 11)
FONT_SMALL     = ("Segoe UI", 9)

# --- Local Cache --------------------------------------------
CACHE_ENCODINGS = True
CACHE_FILE      = "face_cache.pkl"
CACHE_TTL_MINS  = 30

# --- Session (set at runtime after login) -------------------
SESSION = {
    "token":     "",
    "username":  "",
    "com_id":    DEFAULT_COM_ID,
    "branch_id": DEFAULT_BRANCH_ID,
    "loc_id":    DEFAULT_LOC_ID,
    "role":      "",
}
