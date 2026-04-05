@echo off
title Face Attendance System
cd /d "%~dp0"
python main.py
IF ERRORLEVEL 1 (
    echo.
    echo [ERROR] Application crashed. Check the error above.
    pause
)
