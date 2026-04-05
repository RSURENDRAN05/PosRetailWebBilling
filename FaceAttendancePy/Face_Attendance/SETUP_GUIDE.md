# Face Attendance System — Setup Guide

## Architecture Overview

```
[Windows PC]                     [Cloud / cPanel Server]
────────────                     ───────────────────────
Python App (main.py)    ──REST──▶  PHP API (php_backend/)
  Tkinter GUI                        ↕
  OpenCV Webcam                    MySQL Database
  face_recognition lib
```

---

## STEP 1 — Set Up the Database (cPanel)

1. Log into **cPanel → phpMyAdmin**
2. Select your database (or create a new one)
3. Click **SQL** tab and paste the contents of:
   `php_backend/setup/database.sql`
4. Click **Go** — this creates the 3 tables

---

## STEP 2 — Upload PHP Backend to Your Server

1. Upload the entire `php_backend/` folder to your web server
   - Example path: `public_html/face_attendance/`
2. Edit `php_backend/config/database.php`:
   ```php
   define('DB_HOST',    'YOUR_SERVER_IP');   // e.g. 123.45.67.89
   define('DB_PORT',    '3306');
   define('DB_USER',    'YOUR_DB_USERNAME');
   define('DB_PASS',    'YOUR_DB_PASSWORD');
   define('DB_NAME',    'YOUR_DATABASE_NAME');
   define('API_SECRET', 'face_attendance_secret_2024');
   ```
3. Test your API in browser:
   `https://yourdomain.com/face_attendance/api/stats.php?api_key=face_attendance_secret_2024`
   You should see a JSON response with `"success": true`

---

## STEP 3 — Configure Python Client

Edit `python_client/config.py`:
```python
API_BASE_URL = "https://yourdomain.com/face_attendance/api"
API_KEY      = "face_attendance_secret_2024"
```

---

## STEP 4 — Install Python Dependencies (Windows)

**Requirements:** Python 3.9+ (64-bit), Windows 10/11

Option A (Recommended):
```
Double-click:  python_client/install.bat
```

Option B (if Option A fails):
```
Double-click:  python_client/install_alternative.bat
```

Option C (manual):
```bash
pip install cmake
pip install dlib
pip install face_recognition opencv-python Pillow requests numpy
```

> ⚠ **Note:** `dlib` requires C++ build tools. If install fails, install
> [Visual C++ Build Tools](https://visualstudio.microsoft.com/visual-cpp-build-tools/)
> then retry.

---

## STEP 5 — Run the Application

```
Double-click:  python_client/run.bat
```
or:
```bash
cd python_client
python main.py
```

---

## How to Use

### Register an Employee
1. Click **Register Employee** on the dashboard
2. Fill in Employee ID, Full Name, Department, etc.
3. Click **Capture Face** — look directly at the webcam
4. Wait for 5 face samples to be captured (green progress bar)
5. Click **Save Employee** — data is uploaded to cloud

### Mark Attendance (Live Mode)
1. Click **Mark Attendance** on the dashboard
2. The camera starts — face recognition runs in real-time
3. When a known face is detected → attendance is **automatically** recorded
4. Green box = recognized, Red box = unknown
5. Today's log appears on the right side panel

### View Records
1. Click **Attendance Records**
2. Filter by date range, employee ID
3. Quick buttons: Today / This Week / This Month
4. Export to CSV with one click

### Manage Employees
1. Click **Manage Employees**
2. Search, view, deactivate employees
3. Delete face data (employee must re-register face)

---

## Configuration Options (`config.py`)

| Setting | Default | Description |
|---|---|---|
| `FACE_TOLERANCE` | 0.45 | Lower = stricter face matching |
| `FACE_SAMPLES` | 5 | How many face photos to capture on registration |
| `COOLDOWN_SECONDS` | 5 | Prevent duplicate attendance records |
| `CHECK_OUT_MODE` | False | Set True to enable check-out via face |
| `WORK_START_HOUR` | 9 | After this hour = marked as "Late" |
| `CAMERA_INDEX` | 0 | 0 = default webcam, 1 = external USB cam |
| `CACHE_TTL_MINS` | 30 | How often to refresh face data from cloud |

---

## API Endpoints (PHP)

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/employees.php` | List all employees |
| POST | `/api/employees.php` | Register employee |
| GET | `/api/faces.php` | Get all face encodings |
| POST | `/api/faces.php` | Save face encodings |
| GET | `/api/attendance.php` | Get attendance records |
| POST | `/api/attendance.php` | Check in / Check out |
| GET | `/api/stats.php` | Dashboard statistics |

All endpoints require header: `X-API-Key: face_attendance_secret_2024`

---

## Troubleshooting

**"Camera not found"** → Check `CAMERA_INDEX` in config.py. Try 0 or 1.

**"Cannot connect to server"** → Verify `API_BASE_URL` in config.py and test in browser.

**"dlib install failed"** → Run `install_alternative.bat` or install Visual C++ Build Tools.

**Face not recognized** → Lower `FACE_TOLERANCE` to 0.5 or re-register with better lighting.

**"Employee ID already exists"** → Use a unique ID or delete the existing employee first.

---

## Project Structure

```
Face_Attendance/
├── php_backend/
│   ├── config/
│   │   ├── database.php      ← DB credentials (EDIT THIS)
│   │   └── cors.php          ← Headers & helpers
│   ├── api/
│   │   ├── employees.php     ← Employee CRUD
│   │   ├── faces.php         ← Face encoding storage
│   │   ├── attendance.php    ← Check-in/out
│   │   └── stats.php         ← Dashboard stats
│   ├── setup/
│   │   └── database.sql      ← Run in phpMyAdmin
│   └── .htaccess
│
├── python_client/
│   ├── main.py               ← Entry point
│   ├── config.py             ← API URL & settings (EDIT THIS)
│   ├── api_client.py         ← HTTP communication layer
│   ├── face_utils.py         ← Face capture/recognition engine
│   ├── requirements.txt
│   ├── install.bat           ← Windows installer
│   ├── run.bat               ← Windows launcher
│   └── screens/
│       ├── register_screen.py
│       ├── attendance_screen.py
│       ├── records_screen.py
│       └── employees_screen.py
│
└── SETUP_GUIDE.md            ← This file
```
