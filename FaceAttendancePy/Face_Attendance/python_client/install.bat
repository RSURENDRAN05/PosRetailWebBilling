@echo off
title Face Attendance - Installer
echo ================================================
echo   Face Attendance System - Python Installer
echo ================================================
echo.

:: Check Python
python --version >nul 2>&1
IF ERRORLEVEL 1 (
    echo [ERROR] Python is not installed or not in PATH.
    echo Please install Python 3.9+ from https://python.org
    pause
    exit /b 1
)

echo [1/4] Upgrading pip...
python -m pip install --upgrade pip

echo.
echo [2/4] Installing cmake (required for dlib)...
pip install cmake

echo.
echo [3/4] Installing dlib...
pip install dlib

echo.
echo [4/4] Installing all dependencies...
pip install -r requirements.txt

echo.
echo ================================================
echo   Installation complete!
echo   Run the app using:  run.bat
echo ================================================
pause
