PUSHD "\\UNCpath"

REN Setup Setup.old

cd 10InstallScripts

powershell -Command "(New-Object Net.WebClient).DownloadFile('https://embed.widencdn.net/download/kavokerr/msivwrexdx/KaVo_IO-Sensor_Driver.1.0.6.96.zip', 'titanium.zip')"

7za e -o"\\UNCpath\Setup.old" titanium.zip -y

..\setup.old\setup.exe /r /f1"\\UNCpath\10InstallScripts\record.iss"

..\setup.old\KaVo_IO-Sensor_Driver.1.0.6.96.exe /r /f1"\\UNCpath\10InstallScripts\recordKavo.iss"

CreateNewISS.exe

:CHECKOS
IF EXIST "%PROGRAMFILES(X86)%" (GOTO 64BIT) ELSE (GOTO 32BIT)

:64BIT

ROBOCOPY 106Fix "%PROGRAMFILES(X86)%\DEXIS" *.dll
GOTO END

:32BIT

ROBOCOPY 106Fix "%PROGRAMFILES%\DEXIS" *.dll
GOTO END

:END

POPD
