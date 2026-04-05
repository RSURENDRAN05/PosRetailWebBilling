# Database Integration Guide
## Face Attendance ↔ Existing POS System (myposqrc_posretail)

---

## Your Existing Tables (READ-ONLY from Face System)

| Table | Purpose | Key Columns Used |
|---|---|---|
| `pos_employeeinfo` | Employee master | `emp_id`, `emp_printname`, `emp_compid`, `emp_locid`, `emp_active` |
| `employee_fingerprints` | Fingerprint BLOBs | `id`, `emp_id`, `finger_template`, `finger_name`, `fingertype` |
| `employee_attendance` | Attendance records | `att_id`, `emp_id`, `com_id`, `loc_id`, `morning_in`, `evening_out`, `total_work_hours` |

---

## What Was Added (run `integrate_existing_db.sql`)

### 1. New Table: `employee_face_encodings`
Mirrors `employee_fingerprints` exactly — same structure, stores face vectors instead of BLOB:

```
employee_fingerprints          employee_face_encodings
─────────────────────          ───────────────────────
id             INT PK          id             INT PK
emp_id         VARCHAR(50) FK  emp_id         VARCHAR(50) FK → pos_employeeinfo
finger_template BLOB           encoding       MEDIUMTEXT  ← JSON [128 floats]
finger_name    VARCHAR(50)     face_name      VARCHAR(50) (e.g. 'face')
fingertype     VARCHAR(20)     facetype       VARCHAR(20) (e.g. 'full','partial')
created_at     TIMESTAMP       created_at     TIMESTAMP
                               com_id         VARCHAR(50) ← for multi-branch
                               loc_id         VARCHAR(50) ← for location filter
                               sample_index   INT         ← 0-9, multiple samples
```

### 2. New Columns Added to `employee_attendance`

| Column | Type | Description |
|---|---|---|
| `att_source` | ENUM | `face` / `fingerprint` / `web` / `mobile` / `manual` |
| `att_method` | VARCHAR(30) | Detail: `face_scan`, `fp_device`, `web_face_api` |
| `latitude` | DECIMAL(10,7) | GPS latitude of check-in |
| `longitude` | DECIMAL(10,7) | GPS longitude of check-in |
| `gps_accuracy` | FLOAT | GPS accuracy in metres |
| `liveness_pass` | TINYINT(1) | 1 = liveness verified, 0 = skipped |

### 3. Views Created

| View | Description |
|---|---|
| `v_face_attendance_today` | Today's attendance joined with employee info + biometric status |
| `v_employee_biometrics` | All employees with face + fingerprint registration counts |
| `v_monthly_attendance_summary` | Monthly report per employee with source breakdown |

### 4. Stored Procedure: `sp_mark_face_attendance`
Called by PHP to check-in/check-out via a single DB call.

---

## How Check-in Works

```
Face detected by Python/Web/Mobile
        ↓
POST /api/attendance.php
  { emp_id: "EMP001", action: "checkin", source: "face" }
        ↓
PHP looks up pos_employeeinfo WHERE emp_id = 'EMP001' AND emp_active = 1
        ↓
INSERT into employee_attendance:
  morning_in = NOW()          ← check-in time
  att_source = 'face'
  com_id     = emp_compid     ← from pos_employeeinfo
  loc_id     = emp_locid      ← from pos_employeeinfo
```

## How Check-out Works

```
Face detected (if CHECK_OUT_MODE=True) or manual
        ↓
POST /api/attendance.php
  { emp_id: "EMP001", action: "checkout", source: "face" }
        ↓
UPDATE employee_attendance SET:
  evening_out      = NOW()
  total_work_hours = TIMESTAMPDIFF(morning_in, NOW()) / 60
```

---

## Employee Registration Flow (Face)

```
Step 1: Employee already exists in pos_employeeinfo (from POS system)
        ↑ Face system is READ-ONLY for this table

Step 2: Python app / Web / Mobile captures face samples (5 photos)
        ↓
Step 3: POST /api/faces.php
  { emp_id: "EMP001", encodings: [[...128 floats...] × 5], replace: true }
        ↓
Step 4: Saved in employee_face_encodings
  emp_id = 'EMP001'
  encoding = '[0.123, 0.456, ...]'  ← 128-float JSON array
  sample_index = 0,1,2,3,4
  face_name = 'face'                ← mirrors finger_name
  facetype  = 'full'                ← mirrors fingertype
```

---

## Employee Table Column Mapping

| Face System Field | Database Column | Table |
|---|---|---|
| `employee_id` / `emp_id` | `emp_id` | `pos_employeeinfo` |
| `employee_name` | `emp_printname` | `pos_employeeinfo` |
| `com_id` | `emp_compid` | `pos_employeeinfo` |
| `loc_id` | `emp_locid` | `pos_employeeinfo` |
| `status` (active) | `emp_active = 1` | `pos_employeeinfo` |
| `check_in` | `morning_in` | `employee_attendance` |
| `check_out` | `evening_out` | `employee_attendance` |
| `work_hours` | `total_work_hours` | `employee_attendance` |

---

## Useful Queries

```sql
-- Today's present employees (face)
SELECT * FROM v_face_attendance_today WHERE att_source = 'face';

-- All employees with/without face registration
SELECT emp_id, full_name, face_sample_count, fingerprint_count
FROM v_employee_biometrics
WHERE face_sample_count = 0;   -- not yet registered for face

-- This month attendance by source
SELECT emp_id, full_name, face_checkins, fp_checkins, late_days
FROM v_monthly_attendance_summary
WHERE month_year = DATE_FORMAT(NOW(), '%Y-%m');

-- Check if employee is registered for both face AND fingerprint
SELECT e.emp_id, e.emp_printname,
  (SELECT COUNT(*) FROM employee_fingerprints WHERE emp_id = e.emp_id) AS fp_count,
  (SELECT COUNT(*) FROM employee_face_encodings WHERE emp_id = e.emp_id) AS face_count
FROM pos_employeeinfo e
WHERE e.emp_active = 1;
```

---

## Steps to Go Live

1. **Run SQL** → Open phpMyAdmin → Select `myposqrc_posretail` → Import `integrate_existing_db.sql`
2. **Verify** → Run the SELECT checks at the bottom of the SQL file
3. **Update PHP config** → Edit `php_backend/config/database.php` with your cPanel credentials
4. **Update Python config** → Edit `python_client/config.py` → set `API_BASE_URL`
5. **Register faces** → Open Python app → Click Register → Search existing employee by ID → Capture face
6. **Mark attendance** → Click Mark Attendance → face scans auto check-in to `morning_in` column

---

## Important Notes

- The Face system **never modifies** `pos_employeeinfo` — employees are managed by your existing POS
- Fingerprint BLOBs in `employee_fingerprints` are **untouched** — fingerprint device still works independently
- Both fingerprint and face attendance write to **the same** `employee_attendance` table
- The `att_source` column tells you which method was used for each record
