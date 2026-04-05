# ============================================================
# Liveness Detection Module
# Detects: Eye Blink, Head Turn, Multiple Face Positions
# Uses: face_recognition + dlib landmarks
# ============================================================

import cv2
import face_recognition
import numpy as np
import time
from config import LIVENESS_BLINK_COUNT, LIVENESS_TIMEOUT_SECS


# ---- Eye Aspect Ratio (EAR) for blink detection -----------
def eye_aspect_ratio(landmarks, eye: str) -> float:
    """
    Compute Eye Aspect Ratio from 68-point face landmarks.
    eye: 'left' or 'right'
    """
    if eye == 'left':
        pts = [landmarks['left_eye'][i] for i in range(6)]
    else:
        pts = [landmarks['right_eye'][i] for i in range(6)]

    A = np.linalg.norm(np.array(pts[1]) - np.array(pts[5]))
    B = np.linalg.norm(np.array(pts[2]) - np.array(pts[4]))
    C = np.linalg.norm(np.array(pts[0]) - np.array(pts[3]))
    return (A + B) / (2.0 * C) if C > 0 else 0.0


def nose_tip(landmarks) -> tuple:
    """Return nose tip x,y from landmarks dict."""
    return landmarks['nose_tip'][2]   # middle of nose tip


# ---- Liveness State Machine --------------------------------

class LivenessChecker:
    EAR_THRESHOLD = 0.22    # below this = eye closed
    BLINK_CONSEC  = 2       # consecutive frames for a blink

    def __init__(self, blinks_required: int = LIVENESS_BLINK_COUNT,
                 require_head_turn: bool = True):
        self.blinks_required  = blinks_required
        self.require_head_turn = require_head_turn
        self.reset()

    def reset(self):
        self.blink_count     = 0
        self.blink_frames    = 0     # consecutive frames eye closed
        self.head_turned     = False
        self.nose_x_history  = []
        self.passed          = False
        self.start_time      = time.time()
        self.status          = "👁 Please blink your eyes"

    @property
    def timed_out(self) -> bool:
        return time.time() - self.start_time > LIVENESS_TIMEOUT_SECS

    def update(self, frame_rgb: np.ndarray) -> dict:
        """
        Process a single RGB frame. Returns:
        {
          'passed': bool,
          'timed_out': bool,
          'status': str,
          'blinks': int,
          'head_turned': bool,
          'face_found': bool,
        }
        """
        if self.passed:
            return self._result(True)
        if self.timed_out:
            return self._result(False, timed_out=True)

        # Detect face + landmarks
        locs  = face_recognition.face_locations(frame_rgb, model='hog')
        if not locs:
            self.status = "🔍 Face not detected — look at camera"
            return self._result(False, face_found=False)

        if len(locs) > 1:
            self.status = "⚠ Multiple faces detected — one person only"
            return self._result(False, face_found=True)

        landmarks = face_recognition.face_landmarks(frame_rgb, locs)[0]

        # ---- Blink detection ----
        left_ear  = eye_aspect_ratio(landmarks, 'left')
        right_ear = eye_aspect_ratio(landmarks, 'right')
        avg_ear   = (left_ear + right_ear) / 2.0

        if avg_ear < self.EAR_THRESHOLD:
            self.blink_frames += 1
        else:
            if self.blink_frames >= self.BLINK_CONSEC:
                self.blink_count += 1
                self.blink_frames = 0

        # ---- Head turn detection (via nose x movement) ----
        if 'nose_bridge' in landmarks and landmarks['nose_bridge']:
            nx = landmarks['nose_bridge'][0][0]
            self.nose_x_history.append(nx)
            if len(self.nose_x_history) > 30:
                self.nose_x_history.pop(0)
            if len(self.nose_x_history) >= 10:
                x_range = max(self.nose_x_history) - min(self.nose_x_history)
                frame_width = frame_rgb.shape[1]
                if x_range > frame_width * 0.12:  # 12% of frame width = significant turn
                    self.head_turned = True

        # ---- Check pass condition ----
        blinks_ok    = self.blink_count >= self.blinks_required
        head_ok      = (not self.require_head_turn) or self.head_turned
        remaining    = self.blinks_required - self.blink_count

        if blinks_ok and head_ok:
            self.passed = True
            self.status = "✅ Liveness verified!"
        elif not blinks_ok:
            self.status = f"👁 Blink {remaining} more time(s)"
            if self.blink_count > 0:
                self.status += f" ({self.blink_count} done)"
        elif not head_ok:
            self.status = "↔ Slowly turn your head left then right"

        return self._result(self.passed)

    def _result(self, passed, timed_out=False, face_found=True):
        return {
            'passed':      passed,
            'timed_out':   timed_out,
            'status':      self.status if not timed_out else "⏰ Liveness check timed out. Try again.",
            'blinks':      self.blink_count,
            'head_turned': self.head_turned,
            'face_found':  face_found,
        }


# ---- Multi-Position Registration Check -------------------

class MultiPositionCollector:
    """
    Guides user through multiple face positions for registration:
    Front, Left, Right, Up, Down — capturing samples at each pose.
    """
    POSITIONS = [
        ("front",   "Look straight at the camera",        None),
        ("left",    "Slowly turn your head LEFT",          "left"),
        ("right",   "Slowly turn your head RIGHT",         "right"),
        ("up",      "Tilt your head slightly UP",          "up"),
        ("down",    "Tilt your head slightly DOWN",        "down"),
    ]
    SAMPLES_PER_POSITION = 2

    def __init__(self):
        self.reset()

    def reset(self):
        self.pos_idx       = 0
        self.pos_samples   = 0
        self.all_encodings = []
        self.done          = False

    @property
    def current_instruction(self) -> str:
        if self.done:
            return f"✅ Done! {len(self.all_encodings)} samples captured"
        if self.pos_idx >= len(self.POSITIONS):
            self.done = True
            return self._result_str()
        name, instr, _ = self.POSITIONS[self.pos_idx]
        return f"[{self.pos_idx+1}/{len(self.POSITIONS)}] {instr} ({self.pos_samples}/{self.SAMPLES_PER_POSITION})"

    def add_encoding(self, encoding):
        self.all_encodings.append(encoding)
        self.pos_samples += 1
        if self.pos_samples >= self.SAMPLES_PER_POSITION:
            self.pos_idx   += 1
            self.pos_samples = 0
            if self.pos_idx >= len(self.POSITIONS):
                self.done = True

    def _result_str(self):
        return f"✅ All positions captured! {len(self.all_encodings)} samples"
