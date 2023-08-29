PUSHD "\\UNCpath"

cd 10InstallScripts

TASKKILL /im "integra.exe"
TASKKILL /im "dexvideo.exe"
TASKKILL /im "Patient Administration.exe"
TASKKILL /im "chart.exe"

CERTUTIL -addstore "TrustedPublisher" vision.cer
CERTUTIL -addstore "TrustedPublisher" mpia.cer
CERTUTIL -addstore "TrustedPublisher" bae.cer

:CHECKOS
IF EXIST "%PROGRAMFILES(X86)%" (GOTO 64BIT) ELSE (GOTO 32BIT)

:64BIT
..\setup.old\setup.exe /s /f1"\\UNCpath\10InstallScripts\record64.iss"
..\setup.old\KaVo_IO-Sensor_Driver.1.0.6.96.exe /s /f1"\\UNCpath\10InstallScripts\recordKavo.iss"

ROBOCOPY 106Fix "%PROGRAMFILES(X86)%\DEXIS" *.dll
GOTO END

:32BIT
..\setup.old\setup.exe /s /f1"\\UNCpath\10InstallScripts\record32.iss"
..\setup.old\KaVo_IO-Sensor_Driver.1.0.6.96.exe /s /f1"\\UNCpath\10InstallScripts\recordKavo.iss"

ROBOCOPY 106Fix "%PROGRAMFILES%\DEXIS" *.dll
GOTO END

:END

POPD
