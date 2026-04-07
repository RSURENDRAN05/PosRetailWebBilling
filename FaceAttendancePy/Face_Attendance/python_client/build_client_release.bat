@echo off
setlocal

title Face Attendance - Client Release Packager
cd /d "%~dp0"

echo ================================================
echo   Face Attendance - Client Release Packager
echo ================================================
echo.

set SRC=%~dp0dist\FaceAttendanceApp
set OUTROOT=%~dp0client_release
set OUT=%OUTROOT%\FaceAttendanceApp

if not exist "%SRC%\FaceAttendanceApp.exe" (
    echo [ERROR] Build output not found:
    echo         %SRC%\FaceAttendanceApp.exe
    echo Build the app first using PyInstaller onedir.
    pause
    exit /b 1
)

if exist "%OUTROOT%" rmdir /s /q "%OUTROOT%"
mkdir "%OUT%"

echo [1/3] Copying app files...
xcopy "%SRC%\*" "%OUT%\" /e /i /h /y >nul

echo [2/3] Copying client docs...
copy /y "%~dp0CLIENT_INSTALL_GUIDE.md" "%OUTROOT%\CLIENT_INSTALL_GUIDE.md" >nul
copy /y "%~dp0CLIENT_DELIVERY_CHECKLIST.md" "%OUTROOT%\CLIENT_DELIVERY_CHECKLIST.md" >nul

echo [3/3] Writing quick start note...
(
    echo Face Attendance - Client Package
    echo.
    echo 1. Open folder: FaceAttendanceApp
    echo 2. Run: FaceAttendanceApp.exe
    echo 3. Keep _internal folder with the exe.
) > "%OUTROOT%\README_FIRST.txt"

echo.
echo Done.
echo Client package created at:
echo %OUTROOT%

echo.
pause
endlocal
