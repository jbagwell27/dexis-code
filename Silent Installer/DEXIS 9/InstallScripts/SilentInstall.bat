@ECHO off
PUSHD "\\UNCPath"

CD Download

InstallScripts\SetPathInline.exe "\\UNCPath"

Robocopy "\\UNCPath\Download\InstallScripts" "C:\DEXIS" record.iss recordKavo.iss

certutil -addstore "TrustedPublisher" InstallScripts\vision.cer >nul 2>nul
certutil -addstore "TrustedPublisher" InstallScripts\mpia.cer >nul 2>nul
certutil -addstore "TrustedPublisher" InstallScripts\bae.cer >nul 2>nul

REM Rename all DEXIS components so they will automatically update
REM without having to prompt
REN C:\DEXIS\DEXvideo.exe C:\DEXIS\DEXvideo(old).exe >nul 2>nul
REN C:\DEXIS\DEXcdt.exe C:\DEXIS\DEXcdt(old).exe >nul 2>nul
REN C:\DEXIS\DEX-iCAT.exe  C:\DEXIS\DEX-iCAT(old).exe >nul 2>nul
REN C:\DEXIS\DEXcranex3D.exe C:\DEXIS\DEXcranex3D(old).exe >nul 2>nul
REN C:\DEXIS\DEXgxdp700.exe C:\DEXIS\DEXgxdp700(old).exe >nul 2>nul
REN C:\DEXIS\DEXop300.exe C:\DEXIS\DEXop300(old).exe >nul 2>nul
REN C:\DEXIS\DEXpanexamplus.exe C:\DEXIS\DEXpanexamplus(old).exe >nul 2>nul
REN C:\DEXIS\DEXgxdp300.exe C:\DEXIS\DEXgxdp300(old).exe >nul 2>nul
REN C:\DEXIS\DEXnovus.exe C:\DEXIS\DEXnovus(old).exe >nul 2>nul
REN C:\DEXIS\DEXpanexam.exe C:\DEXIS\DEXpanexam(old).exe >nul 2>nul
REN C:\DEXIS\DEXdenoptix.exe C:\DEXIS\DEXdenoptix(old).exe >nul 2>nul
REN C:\DEXIS\DEXexpress.exe C:\DEXIS\DEXexpress(old).exe >nul 2>nul
REN C:\DEXIS\DEXgxps500.exe C:\DEXIS\DEXgxps500(old).exe >nul 2>nul
REN C:\DEXIS\DEXoptime.exe C:\DEXIS\DEXoptime(old).exe >nul 2>nul
REN C:\DEXIS\DEXscanexam.exe C:\DEXIS\DEXscanexam(old).exe >nul 2>nul
REN C:\DEXIS\DEXscanx.exe C:\DEXIS\DEXscanx(old).exe >nul 2>nul

REM Rename the Install files
REN KaVo_IO-Sensor_Driver.exe KaVo_IO-Sensor_Driver1.exe >nul 2>nul
REN DEXlib-Platinum.exe DEXlib-Platinum950.exe >nul 2>nul

REM Install DEXIS 9 software
ECHO Installing DEXIS 9 Software Suite...

da950us.exe /s /f1"C:\DEXIS\record.iss"

ECHO

REM Insatll DEXIS Sensor Libraries
ECHO Installing DEXIS 9 Sensor Libraries...

DEXlib-Platinum950.exe /L2057 C:\DEXIS

ECHO

ECHO Installing KaVo Sensor Drivers...
KaVo_IO-Sensor_Driver1.exe /s /f1"C:\DEXIS\recordKavo.iss"

REM  put the modules back to their original state
REN C:\DEXIS\DEXvideo(old).exe C:\DEXIS\DEXvideo.exe >nul 2>nul
REN C:\DEXIS\DEXcdt(old).exe C:\DEXIS\DEXcdt.exe >nul 2>nul
REN C:\DEXIS\DEX-iCAT(old).exe C:\DEXIS\DEX-iCAT.exe >nul 2>nul
REN C:\DEXIS\DEXcranex3D(old).exe C:\DEXIS\DEXcranex3D.exe >nul 2>nul
REN C:\DEXIS\DEXgxdp700(old).exe C:\DEXIS\DEXgxdp700.exe >nul 2>nul
REN C:\DEXIS\DEXop300(old).exe C:\DEXIS\DEXop300.exe >nul 2>nul
REN C:\DEXIS\DEXpanexamplus(old).exe C:\DEXIS\DEXpanexamplus.exe >nul 2>nul
REN C:\DEXIS\DEXgxdp300(old).exe C:\DEXIS\DEXgxdp300.exe >nul 2>nul
REN C:\DEXIS\DEXnovus(old).exe C:\DEXIS\DEXnovus.exe >nul 2>nul
REN C:\DEXIS\DEXpanexam(old).exe C:\DEXIS\DEXpanexam.exe >nul 2>nul
REN C:\DEXIS\DEXdenoptix(old).exe C:\DEXIS\DEXdenoptix.exe >nul 2>nul
REN C:\DEXIS\DEXexpress(old).exe C:\DEXIS\DEXexpress.exe >nul 2>nul
REN C:\DEXIS\DEXgxps500(old).exe C:\DEXIS\DEXgxps500.exe >nul 2>nul
REN C:\DEXIS\DEXoptime(old).exe C:\DEXIS\DEXoptime.exe >nul 2>nul
REN C:\DEXIS\DEXscanexam(old).exe C:\DEXIS\DEXscanexam.exe >nul 2>nul
REN C:\DEXIS\DEXscanx(old).exe C:\DEXIS\DEXscanx.exe >nul 2>nul

DEL C:\dexis\record.iss
DEL C:\dexis\recordKavo.iss

POPD

ECHO Install finished
PAUSE

REM shutdown /r -f -t 0