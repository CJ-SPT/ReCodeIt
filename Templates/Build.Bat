@echo off
setlocal

set executable_name="ReCodeIt.exe"  

rem Check if the executable is in the PATH
where /q %executable_name%

if %errorlevel% equ 0 (
    rem Executable found in PATH, run it with arguments
    echo Found "%executable_name%" in the system PATH
    %executable_name% %*
) else (
    rem Executable not found in PATH
    echo Error: "%executable_name%" not found in the system PATH.
)
