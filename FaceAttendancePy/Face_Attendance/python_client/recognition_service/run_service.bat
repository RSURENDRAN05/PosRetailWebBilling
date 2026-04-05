@echo off
title Face Attendance - Recognition Service
cd /d "%~dp0\.."
echo =============================================
echo  Face Attendance Recognition Service
echo  Mobile and Web apps call this for recognition
echo  Keep this window OPEN while using mobile/web
echo =============================================
echo.
pip install flask flask-cors --quiet
python recognition_service/app.py
pause
