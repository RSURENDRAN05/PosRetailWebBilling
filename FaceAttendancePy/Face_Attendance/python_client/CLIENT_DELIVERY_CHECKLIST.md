# Client Delivery Checklist - Face Attendance

Use this checklist before handing the client package to a customer.

## 1) Build Verification

- Confirm app build folder exists: dist/FaceAttendanceApp/
- Confirm launcher exists: dist/FaceAttendanceApp/FaceAttendanceApp.exe
- Confirm runtime folder exists: dist/FaceAttendanceApp/_internal/
- Launch app once on your machine and confirm dashboard opens.

## 2) Configuration Verification

- Open config.py
- Verify API_BASE_URL points to production server.
- Verify API_KEY matches server API secret.
- Verify camera index (CAMERA_INDEX) for default client hardware.

## 3) Package Contents to Share With Client

Copy only this folder to client machine:

- dist/FaceAttendanceApp/

Do not share build/ folder for client usage.

## 4) Client Machine Prerequisites

- Windows 10/11 (64-bit)
- Webcam connected and allowed in Windows Privacy Settings
- Internet access to API server
- Microsoft Visual C++ Redistributable 2015-2022 (x64)

## 5) Client-Side Smoke Test

- Run FaceAttendanceApp.exe
- Login works
- Employees list loads
- Camera preview opens in attendance screen
- Attendance records screen loads without error
- Export CSV works

## 6) Handover Items

- App folder: FaceAttendanceApp
- One-page install notes: CLIENT_INSTALL_GUIDE.md
- Support contact details
- Version and date of release

## 7) Common Client Issues

- App does not open: confirm _internal folder is next to FaceAttendanceApp.exe
- DLL error: install/reinstall VC++ Redistributable x64
- No camera: check CAMERA_INDEX in config.py and Windows camera permissions
- API error: validate API_BASE_URL and firewall/network access
