@echo off
set CONVERT="%~dp0\internal_convert.bat"
if [%1]==[] goto :eof
:loop
call %CONVERT% anim %1
shift
if not [%1]==[] goto loop