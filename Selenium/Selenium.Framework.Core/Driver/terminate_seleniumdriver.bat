
REM TASKKILL /IM chromedriver.exe /F
REM pause


echo off
tasklist /fi "imagename eq chromedriver.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "chromedriver.exe"
pause



echo off
tasklist /fi "imagename eq operadriver.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "operadriver.exe"
pause