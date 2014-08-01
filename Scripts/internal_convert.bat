@echo off

REM configure these paths
set FBXCONVERTER="C:\Program Files\Autodesk\FBX\FBX Converter\2013.2\bin\FbxConverter.exe"
set BLENDER249b="C:\Program Files (x86)\Blender Foundation\Blender 2.49b\blender"
set BLENDER265="C:\Program Files\Blender Foundation\Blender 2.65\blender"
set UNZIP="C:\Program Files\7-Zip\7z.exe"

REM part of this toolkit. path should be correct. the AnimationExtractor must be built once with visual studio.
set ANIMEXTRACTOR="%~dp0\..\BaamStudios.AnimationExtractor\bin\Debug\BaamStudios.AnimationExtractor.exe"

REM check output file format
set mode=
if "%1" == "fbx" (	
	set mode=fbx
) else (
	if "%1" == "anim" (
		set mode=anim
	)
)
if "%mode%" == "" (
	echo first parameter must be "fbx" or "anim".
	pause
	goto :eof
)

REM check input file
set ZIP=%2
set ZIP=%ZIP:"=%
if not exist "%ZIP%" (
	echo second parameter must be a zip file.
	pause
	goto :eof
)

REM unzip
set ZIP_EX=%ZIP%_ex
rmdir /s /q "%ZIP_EX%"
%UNZIP% x "%ZIP%" -o"%ZIP_EX%" -r

cd "%ZIP_EX%"

REM find dae
for %%i in (*.dae) do (
	move "%%i" "%~n2.dae"
	goto :break
)
:break

REM import mixamo dae
%BLENDER249b% -P "%~dp0\internal_dae2blend.py" -- "%~n2.dae" "%~n2.blend"
	
REM make fbx
if "%mode%" == "fbx" (
	%BLENDER265% --background --python "%~dp0\internal_blend2tex.py" -- "%~n2.blend" "%~n2.fbx"
	%FBXCONVERTER% "%~n2.fbx" "%~n2.bin.fbx" /e /f201100
	move "%~n2.bin.fbx" "..\%~n2.fbx"
)

REM make anim
if "%mode%" == "anim" (
	%BLENDER265% --background --python "%~dp0\internal_blend2anim.py" -- "%~n2.blend" "%~n2.fbx"
	%FBXCONVERTER% "%~n2.fbx" "%~n2.bin.fbx" /e /f201100
	%ANIMEXTRACTOR% "%ZIP%_ex\%~n2.bin.fbx"
	move "%~n2.bin.anim" "..\%~n2.anim"
)

REM cleanup
cd..
rmdir /s /q "%ZIP_EX%"
