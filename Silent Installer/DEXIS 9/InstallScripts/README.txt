This is the silent installer for DEXIS 9
(c) Joshua Bagwell 2019

Do not use if the computer has existing Titanium Drivers installed. Uninstall the drivers before continuing this Process.

The steps for use are as follows:

1: Install DEXIS through DEXmenu on either the server or on a single workstation.
This will create the files we need: da950US.exe, DEXlib-Platinum.exe and KaVo_IO-Sensor_Driver.exe.

2: Copy the installScripts folder into the download folder on the server. eg: \\server\dexis\data\download

3: Run ChangePath.exe
	i:	Set the path to the <<UNC>> path to the data folder. DO NOT ADD AN ENDING SLASH {'\'}
	ii:	To paste the path fron clipboard: Right-click the titlebar>>Edit>>Paste
	
3a: If the you need to manually change the path:
	i:	Right click on SilentInstall.bat, and SilentInstallRecord.bat and click edit.
	ii:	Press Ctrl+H to open the Find/Replace window
	iii:	Find "\\UNCpath" and replace all with the data folder path. DO NOT ADD AN ENDING SLASH {'\'}

4: On one workstation that will use the new Titanium sensor, run 'SilentInstallRecord.bat' as Administrator.
	This will set the template for the other workstations and is a very important step.
	i:	Follow the setup prompts as they appear. The setup for the Sensor libraries will not appear, this is normal.
	ii:	Select 'Restart Later' when prompted.

5: On all other workstations, run 'SilentInstall.bat' as Administrator
	i:	This can be run simultaneously on all machines.
	ii:	There will be a few windows that appear with loading bars, but they will not prompt for any user input.

6: After the command prompt closes, check Programs and Features to confirm the proper version is installed.

7: Permanently delete the transferred folder. DO NOT LEAVE ANY COPIES

8: Possible Issues:
	> Prompt with Multiple Sensor libraries appears
		- Cancel installation, and close the Command Prompt
		- Uninstall ALL sensor libraries (DO NOT UNINSTALL DEXIS)
		- Restart the installation using the script.

7: If DEXIS does not get installed, note the version of DEXIS and Sensor Libraries that are installed.
	i:	If this happens uninstall DEXIS and the KaVo Drivers, then Manually run the install via DEXmenu on the CD files and install the software manually.


If there are any issues, please note the following:
	- Version of Windows.
	- Version of DEXIS and Libraries (If install fails)
	- Installed Modules and versions of modules: ex: DEXcapture 223, Dentrix Integrator 323, etc.
	- Save them in a text file with the case number in the log folder in my P Drive.