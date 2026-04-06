@echo off
title Face Attendance System
cd /d "%~dp0"
set PYTHON=c:\Python\Python314\python.exe
"%PYTHON%" main.py
IF ERRORLEVEL 1 (
    echo.
    echo [ERROR] Application crashed. Check the error above.
    pause
)
