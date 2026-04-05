# ============================================================
# Face Recognition Microservice
# Runs on Windows PC — mobile/web apps call this for recognition
# Start: run_service.bat   or   python app.py
# Listens on: http://0.0.0.0:5001
# ============================================================

import sys
import os
sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..'))

from flask import Flask, request, jsonify
from flask_cors import CORS
import face_recognition
import numpy as np
import base64
import cv2
import time
from threading import Lock
from config import (
    API_BASE_URL, API_KEY, FACE_TOLERANCE, CACHE_TTL_MINS
)
from api_client import APIClient
from face_utils import face_cache

app = Flask(__name__)
CORS(app)   # Allow all origins (restrict in production)

_lock = Lock()

# ============================================================
# STARTUP — pre-load face encodings
# ============================================================

@app.before_request
def ensure_loaded():
    if face_cache.is_stale():
        with _lock:
            if face_cache.is_stale():
                ok, msg = face_cache.load_from_api()
                app.logger.info(f"Face cache: {msg}")


# ============================================================
# ROUTES
# ============================================================

@app.route('/health', methods=['GET'])
def health():
    data = face_cache.get_data()
    return jsonify({
        'status':   'ok',
        'faces':    len(data),
        'api_url':  API_BASE_URL,
        'timestamp': time.time(),
    })


@app.route('/reload', methods=['POST'])
def reload_faces():
    """Force-reload face encodings from cloud."""
    face_cache.clear()
    ok, msg = face_cache.load_from_api()
    data = face_cache.get_data()
    return jsonify({'success': ok, 'message': msg, 'faces': len(data)})


@app.route('/recognize', methods=['POST'])
def recognize():
    """
    Accepts a base64-encoded image (JPEG/PNG).
    Returns the best-matching employee or 'unknown'.

    Request JSON:
      { "image": "<base64_string>", "com_id": "COM001", "branch_id": "BR001" }

    Response:
      { "success": true, "emp_id": "EMP001", "employee_name": "John", "confidence": 92.3 }
    """
    body = request.get_json(force=True, silent=True) or {}
    img_b64 = body.get('image', '')

    if not img_b64:
        return jsonify({'success': False, 'message': 'image field required'}), 400

    # Decode base64 → numpy array
    try:
        # Handle data URI prefix
        if ',' in img_b64:
            img_b64 = img_b64.split(',', 1)[1]
        img_bytes = base64.b64decode(img_b64)
        np_arr   = np.frombuffer(img_bytes, np.uint8)
        frame    = cv2.imdecode(np_arr, cv2.IMREAD_COLOR)
        if frame is None:
            raise ValueError("Could not decode image")
    except Exception as e:
        return jsonify({'success': False, 'message': f'Image decode error: {e}'}), 400

    # Convert to RGB
    rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

    # Detect faces
    locations = face_recognition.face_locations(rgb, model='hog')
    if not locations:
        return jsonify({'success': False, 'message': 'No face detected', 'emp_id': None})

    # Use the largest face
    encodings = face_recognition.face_encodings(rgb, locations)
    if not encodings:
        return jsonify({'success': False, 'message': 'Could not encode face', 'emp_id': None})

    query_enc = encodings[0]

    # Get known faces (optionally filter by com_id / branch_id from request)
    known = face_cache.get_data()
    com_id    = body.get('com_id', '')
    branch_id = body.get('branch_id', '')
    if com_id:
        known = [f for f in known if f.get('com_id') == com_id]
    if branch_id:
        known = [f for f in known if f.get('branch_id') == branch_id]

    if not known:
        return jsonify({'success': False, 'message': 'No registered faces for this context', 'emp_id': None})

    known_encs  = [f['encoding'] for f in known]
    distances   = face_recognition.face_distance(known_encs, query_enc)
    best_idx    = int(np.argmin(distances))
    best_dist   = float(distances[best_idx])

    if best_dist <= FACE_TOLERANCE:
        emp         = known[best_idx]
        confidence  = round((1 - best_dist) * 100, 1)
        return jsonify({
            'success':       True,
            'emp_id':   emp['emp_id'],
            'employee_name': emp['employee_name'],
            'confidence':    confidence,
            'distance':      best_dist,
        })

    return jsonify({
        'success':     False,
        'message':     f'No match (best distance: {best_dist:.3f})',
        'emp_id': None,
    })


@app.route('/encode', methods=['POST'])
def encode_image():
    """
    Extract face encoding from a base64 image.
    Used by web/mobile registration to capture face encodings
    without needing local Python library.

    Request: { "image": "<base64>" }
    Response: { "success": true, "encoding": [128 floats] }
    """
    body   = request.get_json(force=True, silent=True) or {}
    img_b64 = body.get('image', '')
    if not img_b64:
        return jsonify({'success': False, 'message': 'image required'}), 400

    try:
        if ',' in img_b64:
            img_b64 = img_b64.split(',', 1)[1]
        img_bytes = base64.b64decode(img_b64)
        np_arr    = np.frombuffer(img_bytes, np.uint8)
        frame     = cv2.imdecode(np_arr, cv2.IMREAD_COLOR)
        rgb       = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
    except Exception as e:
        return jsonify({'success': False, 'message': str(e)}), 400

    locations = face_recognition.face_locations(rgb, model='hog')
    if not locations:
        return jsonify({'success': False, 'message': 'No face found in image'})

    if len(locations) > 1:
        return jsonify({'success': False, 'message': 'Multiple faces detected — use one person only'})

    encs = face_recognition.face_encodings(rgb, locations)
    if not encs:
        return jsonify({'success': False, 'message': 'Could not encode face'})

    return jsonify({'success': True, 'encoding': encs[0].tolist()})


# ============================================================
# MAIN
# ============================================================

if __name__ == '__main__':
    print("=" * 55)
    print("  Face Attendance Recognition Service")
    print(f"  API Backend: {API_BASE_URL}")
    print("  Listening on: http://0.0.0.0:5001")
    print("  Mobile/Web → http://<YOUR-PC-IP>:5001")
    print("=" * 55)

    # Pre-load cache
    ok, msg = face_cache.load_from_api()
    print(f"  Face cache: {msg}")
    print("=" * 55)

    app.run(host='0.0.0.0', port=5001, debug=False, threaded=True)
