@echo off
setlocal
if "%KeePassDir%"=="" (
  echo Set KeePassDir to the folder containing KeePass.exe and KeePassLib.dll.
  echo Example: set KeePassDir=C:\Dev\KeePass258
  exit /b 1
)
where msbuild >nul 2>nul
if errorlevel 1 (
  echo MSBuild was not found. Install Visual Studio with ".NET desktop development".
  exit /b 1
)
msbuild PasswordAlphabetHighlighter.csproj /t:Rebuild /p:Configuration=Release /p:KeePassDir="%KeePassDir%"
if errorlevel 1 exit /b 1
echo.
echo Built: bin\Release\PasswordAlphabetHighlighter.dll
