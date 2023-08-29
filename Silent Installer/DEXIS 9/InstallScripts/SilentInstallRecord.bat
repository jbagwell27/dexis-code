@ECHO off
PUSHD "\\UNCPath"

CD Download

InstallScripts\SetPathInline.exe "\\UNCPath"

REM Rename all DEXIS components so they will automatically update
REM without having to prompt
REN DEXvideo.exe DEXvideo(old).exe >nul 2>nul
REN DEXcdt.exe DEXcdt(old).exe >nul 2>nul
REN DEX-iCAT.exe  DEX-iCAT(old).exe >nul 2>nul
REN DEXcranex3D.exe DEXcranex3D(old).exe >nul 2>nul
REN DEXgxdp700.exe DEXgxdp700(old).exe >nul 2>nul
REN DEXop300.exe DEXop300(old).exe >nul 2>nul
REN DEXpanexamplus.exe DEXpanexamplus(old).exe >nul 2>nul
REN DEXgxdp300.exe DEXgxdp300(old).exe >nul 2>nul
REN DEXnovus.exe DEXnovus(old).exe >nul 2>nul
REN DEXpanexam.exe DEXpanexam(old).exe >nul 2>nul
REN DEXdenoptix.exe DEXdenoptix(old).exe >nul 2>nul
REN DEXexpress.exe DEXexpress(old).exe >nul 2>nul
REN DEXgxps500.exe DEXgxps500(old).exe >nul 2>nul
REN DEXoptime.exe DEXoptime(old).exe >nul 2>nul
REN DEXscanexam.exe DEXscanexam(old).exe >nul 2>nul
REN DEXscanx.exe DEXscanx(old).exe >nul 2>nul

REM Rename the Install files
REN KaVo_IO-Sensor_Driver.exe KaVo_IO-Sensor_Driver1.exe >nul 2>nul
REN DEXlib-Platinum.exe DEXlib-Platinum950.exe >nul 2>nul

REM Install DEXIS 9 Software
ECHO Installing DEXIS 9 Software Suite...

da950us.exe /r /f1"\\UNCPath\Download\InstallScripts\record.iss"

ECHO

REM Insatll DEXIS Sensor Libraries
ECHO Installing DEXIS 9 Sensor Libraries...

DEXlib-Platinum950.exe /L2057 C:\DEXIS

ECHO

ECHO Installing KaVo Sensor Drivers...
KaVo_IO-Sensor_Driver1.exe /r /f1"\\UNCPath\Download\InstallScripts\recordKavo.iss"

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

POPD

ECHO Installer Recording complete Please run 'SilentInstall.bat' as administrator on all other machines
PAUSE