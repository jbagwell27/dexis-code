@ECHO Off 

rem Author: Joshua Bagwell
rem Version: 1.0b
rem Purpose: To perform a full backup of DEXIS 9 data

ECHO This will Backup your DEXIS to another folder.

ECHO
set result=
for /f %%i in ('FINDSTR xdata C:\dexis\dexis.ini') do (set result=%%i)
set workDir=%result:~6%
echo Working Directory = "%workDir%"
echo

set /p savePath= Where would you like to save the Backup? 

robocopy "%workDir%" "%savepath%" /E

echo BACKUP COMPLETE!!
pause

rem This program is proof of how dedicated I am to the team and the company.
rem I cannot wait to officially become a member of the team, rather than a temporary worker
rem -Josh
