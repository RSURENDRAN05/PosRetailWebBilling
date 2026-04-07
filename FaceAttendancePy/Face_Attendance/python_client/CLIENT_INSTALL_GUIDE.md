# Client Install Guide - Face Attendance

## Quick Install (Recommended)

1. Copy folder FaceAttendanceApp to client PC.
2. Open FaceAttendanceApp folder.
3. Run FaceAttendanceApp.exe.

Important: Keep _internal folder in the same directory as FaceAttendanceApp.exe.

## First-Time Setup on Client PC

1. Install Microsoft Visual C++ Redistributable 2015-2022 (x64).
2. Ensure internet access to your API server URL.
3. Ensure webcam is connected and camera permission is enabled in Windows.

## If Windows Blocks the EXE

1. Right-click FaceAttendanceApp.exe
2. Open Properties
3. Check Unblock (if shown)
4. Click Apply and run again

## Troubleshooting

### Error: Failed to load python314.dll
- Cause: _internal folder missing or separated from exe.
- Fix: Keep complete FaceAttendanceApp folder together.

### Error: DLL load failed while importing _dlib_pybind11
- Install or repair VC++ Redistributable x64.
- Try using full folder build (onedir) instead of onefile exe for that client.

### Camera not detected
- Close other apps using camera.
- Change CAMERA_INDEX in config.py (0 or 1).
- Check Windows camera privacy settings.

### Cannot connect to server
- Verify API_BASE_URL in config.py
- Check internet/firewall/proxy access

## Optional Source Mode Install (Developer)

1. Install Python at C:\Python\Python314\python.exe
2. Run install.bat
3. Run run.bat
