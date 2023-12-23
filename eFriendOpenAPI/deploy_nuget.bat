
SET BUILDCONFIG=Release
SET PRJNAME=eFriendOpenAPI

FOR /F %%I IN ("%0") DO SET CURRENTDIR=%%~dpI

del /Q %CURRENTDIR%bin\%BUILDCONFIG%\*.nupkg

dotnet pack
if %ERRORLEVEL% GTR 0 goto BuildError

for /f %%i in ('powershell.exe -ExecutionPolicy RemoteSigned -file %CURRENTDIR%..\getPrjVersion.ps1 %CURRENTDIR%%PRJNAME%.csproj') do set NUGETVERSION=%%i
echo %NUGETVERSION%

for /f %%i in ('powershell.exe -ExecutionPolicy RemoteSigned -file %CURRENTDIR%..\readFile.ps1 d:\settings\nuget_key.txt') do set NUGETKEY=%%i
echo %NUGETKEY%

if EXIST D:\myNuget\ (
robocopy %CURRENTDIR%bin\%BUILDCONFIG%\ D:\myNuget %PRJNAME%.%NUGETVERSION%.nupkg
) ELSE (
    echo No local nuget directory
)

if '%1' == 'local' goto EndOfBuild

dotnet nuget push %CURRENTDIR%bin\%BUILDCONFIG%\%PRJNAME%.%NUGETVERSION%.nupkg --api-key %NUGETKEY% --source https://api.nuget.org/v3/index.json
goto EndOfBuild

:BuildError
Echo Failed to build


:EndOfBuild
