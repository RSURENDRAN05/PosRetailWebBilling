@echo off
title Face Attendance - Installer
echo ================================================
echo   Face Attendance System - Python Installer
echo ================================================
echo.

set PYTHON=c:\Python\Python314\python.exe
set PIP=%PYTHON% -m pip

:: Check Python
"%PYTHON%" --version >nul 2>&1
IF ERRORLEVEL 1 (
    echo [ERROR] Python not found at c:\Python\Python314\python.exe
    echo Please install Python 3.9+ from https://python.org
    pause
    exit /b 1
)

echo [1/4] Upgrading pip...
"%PIP%" install --upgrade pip

echo.
echo [2/4] Installing cmake (required for dlib)...
"%PIP%" install cmake

echo.
echo [3/4] Installing dlib...
"%PIP%" install dlib

echo.
echo [4/4] Installing all dependencies...
"%PIP%" install -r requirements.txt

echo.
echo ================================================
echo   Installation complete!
echo   Run the app using:  run.bat
echo ================================================
pause
