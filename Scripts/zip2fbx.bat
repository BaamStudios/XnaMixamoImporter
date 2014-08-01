@echo off
if [%1]==[] goto :eof
:loop
call "%~dp0\internal_convert.bat" fbx %1
shift
if not [%1]==[] goto loop