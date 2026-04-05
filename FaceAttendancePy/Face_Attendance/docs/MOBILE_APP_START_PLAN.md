# Mobile App Start Plan

This project can start from the mobile app, but the current architecture has one important constraint:

- The mobile app uses the PHP REST API for auth, employees, attendance, and stats.
- The mobile app does not perform face recognition on-device.
- Face recognition currently depends on the Python recognition service at `python_client/recognition_service/app.py`.

## What Works Now

- Admin login and device PIN login through `php_backend/api/auth.php`
- Dashboard stats through `php_backend/api/stats.php`
- Attendance records through `php_backend/api/attendance.php`
- Employee lookup and face encoding storage through `php_backend/api/employees.php` and `php_backend/api/faces.php`

## Current Mobile-First Architecture

```text
Mobile App (Expo)
  -> PHP REST API
     -> MySQL

Mobile App (face scan)
  -> Python recognition service
     -> PHP REST API /faces.php for cached encodings
```

## Immediate Launch Recommendation

Use this phased approach:

1. Launch the mobile app first for login, dashboard, records, and attendance submission.
2. Keep the Python recognition service running as the recognition backend during phase 1.
3. Treat the PHP API as the system of record for attendance and employee data.

## Phase 1 Checklist

1. Replace placeholder values in `mobile_app/src/api/client.js`.
   - Set `API_BASE` to your public PHP API URL.
   - Set `RECOG_SERVICE` to the reachable IP or hostname of the recognition service.
2. Replace placeholder values in `php_backend/config/database.php` and `php_backend/config/auth.php`.
3. Verify these endpoints from a phone or emulator:
   - `POST /api/auth.php`
   - `GET /api/stats.php`
   - `GET /api/attendance.php`
   - `POST /api/attendance.php`
   - `GET /api/faces.php`
4. Confirm the phone can reach the recognition service over the network.
5. Register at least one employee face sample before testing scan attendance.

## Biggest Remaining Blockers

1. The mobile app still depends on the separate recognition service.
2. Secrets and base URLs are still stored directly in source files.
3. There is no offline queue for failed attendance submissions.
4. There are no automated tests for the mobile or PHP paths.

## Recommended Next Build Steps

1. Move API URLs and secrets to environment-based config.
2. Add a lightweight health endpoint for the recognition service and PHP API.
3. Add an offline queue for attendance POST requests in the mobile app.
4. Decide whether recognition stays as a separate service or moves to a centralized backend.

## Decision Point

If you want a pure mobile + REST architecture with no Windows recognition service, that will require a new backend recognition strategy. The current codebase is mobile-first for UI, but not mobile-only for face matching.