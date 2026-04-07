# ============================================================
# Face Utilities - Capture, Encode, Detect, Match
# ============================================================

import cv2
import numpy as np
import pickle
import os
import json
import time
from datetime import datetime
from threading import Lock
from config import (
    FACE_TOLERANCE, FACE_SAMPLES, FACE_CAPTURE_DELAY,
    FRAME_WIDTH, FRAME_HEIGHT, CAMERA_INDEX,
    CACHE_ENCODINGS, CACHE_FILE, CACHE_TTL_MINS
)
from api_client import APIClient

try:
    import face_recognition
    _FACE_LIB_ERROR = None
except Exception as e:
    face_recognition = None
    _FACE_LIB_ERROR = str(e)


def face_engine_available() -> tuple[bool, str]:
    if face_recognition is None:
        return False, _FACE_LIB_ERROR or "face_recognition/dlib is not available"
    return True, "OK"


class FaceCache:
    """Thread-safe cache for face encodings loaded from cloud."""

    def __init__(self):
        self._lock     = Lock()
        self._data     = []     # list of {"emp_id", "employee_name", "encoding" (np.array)}
        self._loaded   = None   # datetime of last load

    def is_stale(self) -> bool:
        if self._loaded is None:
            return True
        elapsed = (datetime.now() - self._loaded).total_seconds() / 60
        return elapsed >= CACHE_TTL_MINS

    def load_from_api(self) -> tuple[bool, str]:
        """Download all face encodings from cloud and cache them."""
        result = APIClient.get_all_face_encodings()
        if not result.get("success"):
            return False, result.get("message", "Failed to load encodings")

        rows = result.get("data", [])
        entries = []
        for row in rows:
            enc = row.get("encoding")

            # API may return encoding either as list[float] or JSON string.
            if isinstance(enc, str):
                try:
                    enc = json.loads(enc)
                except Exception:
                    enc = None

            if isinstance(enc, list) and len(enc) == 128:
                entries.append({
                    "emp_id":        row["emp_id"],
                    "employee_name": row.get("employee_name", row.get("emp_printname", row["emp_id"])),
                    "department":    row.get("emp_designation", row.get("department", "")),
                    "encoding":      np.array(enc, dtype=np.float64),
                })

        with self._lock:
            self._data   = entries
            self._loaded = datetime.now()

        # Persist to disk
        if CACHE_ENCODINGS:
            try:
                with open(CACHE_FILE, "wb") as f:
                    pickle.dump({"data": entries, "loaded": self._loaded}, f)
            except Exception:
                pass

        return True, f"Loaded {len(entries)} face encoding(s)"

    def load_from_disk(self) -> bool:
        """Try to restore cache from local pickle file."""
        if not os.path.exists(CACHE_FILE):
            return False
        try:
            with open(CACHE_FILE, "rb") as f:
                saved = pickle.load(f)
            with self._lock:
                self._data   = saved["data"]
                self._loaded = saved["loaded"]
            return True
        except Exception:
            return False

    def get_data(self) -> list:
        with self._lock:
            return list(self._data)

    def clear(self):
        with self._lock:
            self._data   = []
            self._loaded = None
        if os.path.exists(CACHE_FILE):
            try:
                os.remove(CACHE_FILE)
            except Exception:
                pass


# Global cache instance
face_cache = FaceCache()


# ============================================================
# ENCODING UTILITIES
# ============================================================

def capture_face_encodings(num_samples: int = FACE_SAMPLES,
                            progress_callback=None) -> tuple[list, str]:
    """
    Open webcam, capture `num_samples` valid face frames,
    return (list_of_128_dim_lists, message).
    progress_callback(current, total) called each capture.
    """
    ok, msg = face_engine_available()
    if not ok:
        return [], f"Face engine unavailable: {msg}"

    cap = cv2.VideoCapture(CAMERA_INDEX)
    cap.set(cv2.CAP_PROP_FRAME_WIDTH,  FRAME_WIDTH)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, FRAME_HEIGHT)

    if not cap.isOpened():
        return [], "Camera not found. Check CAMERA_INDEX in config.py"

    encodings = []
    attempts  = 0
    max_attempts = num_samples * 10

    try:
        while len(encodings) < num_samples and attempts < max_attempts:
            ret, frame = cap.read()
            if not ret:
                time.sleep(0.05)
                attempts += 1
                continue

            # Convert BGR → RGB for face_recognition
            rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

            # Detect face locations (use 'hog' model - fast, CPU-only)
            locations = face_recognition.face_locations(rgb, model='hog')

            if len(locations) == 1:   # Exactly one face
                enc_list = face_recognition.face_encodings(rgb, locations)
                if enc_list:
                    encodings.append(enc_list[0].tolist())
                    if progress_callback:
                        progress_callback(len(encodings), num_samples)
                    time.sleep(FACE_CAPTURE_DELAY)

            elif len(locations) > 1:
                pass  # Multiple faces, skip

            attempts += 1

    finally:
        cap.release()

    if len(encodings) < num_samples:
        return encodings, f"Only captured {len(encodings)}/{num_samples} samples. Ensure good lighting and look at the camera."
    return encodings, "OK"


def encode_frame(frame_bgr: np.ndarray) -> tuple[list, list]:
    """
    Given a BGR frame, return (face_locations, face_encodings_as_np_arrays).
    """
    ok, _ = face_engine_available()
    if not ok:
        return [], []

    rgb = cv2.cvtColor(frame_bgr, cv2.COLOR_BGR2RGB)
    # Resize to small for speed, then scale locations back
    small = cv2.resize(rgb, (0, 0), fx=0.5, fy=0.5)
    locations = face_recognition.face_locations(small, model='hog')
    # Scale up
    locations = [(t*2, r*2, b*2, l*2) for (t, r, b, l) in locations]
    encs = face_recognition.face_encodings(rgb, locations)
    return locations, encs


# ============================================================
# RECOGNITION
# ============================================================

def identify_face(encoding: np.ndarray,
                  known_data: list,
                  tolerance: float = FACE_TOLERANCE) -> tuple[str, str, float]:
    """
    Compare `encoding` against known_data list.
    Returns (emp_id, employee_name, confidence_pct).
    confidence_pct is 0–100 (100 = perfect match).
    """
    ok, _ = face_engine_available()
    if not ok:
        return "Unknown", "Unknown", 0.0

    if not known_data:
        return "Unknown", "Unknown", 0.0

    known_encs  = [d["encoding"] for d in known_data]
    distances   = face_recognition.face_distance(known_encs, encoding)
    best_idx    = int(np.argmin(distances))
    best_dist   = distances[best_idx]

    if best_dist <= tolerance:
        emp     = known_data[best_idx]
        conf    = round((1 - best_dist) * 100, 1)
        return emp["emp_id"], emp["employee_name"], conf
    return "Unknown", "Unknown", 0.0


# ============================================================
# LIVE FRAME ANNOTATION
# ============================================================

def annotate_frame(frame: np.ndarray,
                   locations: list,
                   identities: list,
                   color_map: dict = None) -> np.ndarray:
    """
    Draw bounding boxes and name labels on frame.
    identities: list of (employee_id, name, confidence)
    """
    out = frame.copy()
    for (top, right, bottom, left), (emp_id, name, conf) in zip(locations, identities):
        if emp_id == "Unknown":
            color = (0, 0, 220)   # Red
            label = "Unknown"
        else:
            color = (0, 200, 50)  # Green
            label = f"{name}  {conf:.0f}%"

        # Box
        cv2.rectangle(out, (left, top), (right, bottom), color, 2)

        # Label background
        label_y = top - 10 if top > 30 else bottom + 25
        (w, h), _ = cv2.getTextSize(label, cv2.FONT_HERSHEY_SIMPLEX, 0.6, 1)
        cv2.rectangle(out, (left, label_y - h - 6), (left + w + 6, label_y + 2), color, -1)
        cv2.putText(out, label, (left + 3, label_y - 2),
                    cv2.FONT_HERSHEY_SIMPLEX, 0.6, (255, 255, 255), 1)
    return out


# ============================================================
# CAMERA PREVIEW GENERATOR
# ============================================================

class CameraStream:
    """Thread-safe OpenCV camera wrapper for Tkinter."""

    def __init__(self):
        self.cap  = None
        self._open = False

    def open(self) -> tuple[bool, str]:
        self.cap = cv2.VideoCapture(CAMERA_INDEX)
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH,  FRAME_WIDTH)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT, FRAME_HEIGHT)
        if not self.cap.isOpened():
            return False, "Cannot open camera"
        self._open = True
        return True, "Camera opened"

    def read(self) -> tuple[bool, np.ndarray]:
        if not self._open or self.cap is None:
            return False, None
        return self.cap.read()

    def release(self):
        if self.cap:
            self.cap.release()
        self._open = False

    @property
    def is_open(self) -> bool:
        return self._open
