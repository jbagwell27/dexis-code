echo running test

(New-Object Net.WebClient).DownloadFile('https://embed.widencdn.net/download/kavokerr/msivwrexdx/KaVo_IO-Sensor_Driver.1.0.6.96.zip', 'titanium.zip')

7za e -o"\\win10\dexis\Setup.old" titanium.zip -y

pause