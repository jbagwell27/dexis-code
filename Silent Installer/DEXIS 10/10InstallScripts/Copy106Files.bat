PUSHD "\\server\dexis imaging suite\"

:CHECKOS
IF EXIST "%PROGRAMFILES(X86)%" (GOTO 64BIT) ELSE (GOTO 32BIT)

:64BIT

ROBOCOPY 106Fix "%PROGRAMFILES(X86)%\DEXIS" *.dll
GOTO END

:32BIT

ROBOCOPY 106Fix "%PROGRAMFILES%\DEXIS" *.dll
GOTO END

:END
pause

POPD
