@echo off
title Face Attendance - Alternative Installer (Pre-built dlib wheel)
echo ================================================
echo  Alternative Installer - Uses pre-built dlib wheel
echo  (Use this if the normal install.bat fails)
echo ================================================
echo.

set PYTHON=c:\Python\Python314\python.exe
set PIP=%PYTHON% -m pip

"%PYTHON%" --version >nul 2>&1
IF ERRORLEVEL 1 (
    echo [ERROR] Python not found at c:\Python\Python314\python.exe
    pause
    exit /b 1
)

:: Get Python version
for /f "tokens=2 delims= " %%v in ('"%PYTHON%" --version 2^>^&1') do set PYVER=%%v
echo Detected Python: %PYVER%

echo.
echo [1/3] Installing Visual C++ Build Tools dependency...
echo If prompted, install "Desktop development with C++" workload.
echo Download: https://visualstudio.microsoft.com/visual-cpp-build-tools/
echo.

echo [2/3] Installing packages without dlib first...
"%PIP%" install opencv-python Pillow requests numpy

echo.
echo [3/3] Try installing face_recognition with pre-built wheel...
echo Downloading pre-built dlib for Windows...
"%PIP%" install https://github.com/jloh02/dlib/releases/download/v19.22/dlib-19.22.99-cp39-cp39-win_amd64.whl 2>nul
IF ERRORLEVEL 1 (
    echo Trying pip install dlib directly...
    "%PIP%" install dlib
)
"%PIP%" install face_recognition

echo.
echo ================================================
echo   Done! Run: run.bat
echo ================================================
pause
