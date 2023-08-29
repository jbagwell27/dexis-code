# Author: Joshua Bagwell
# Purpose: To change the data path of proprietary software on workstations
# after a server migration

param([INT32]$o = 0, [String]$dataPath)

function software_nine() {
    if ([System.Environment]::Is64BitOperatingSystem) {

        # This particular software's local path is stored in the registry
        $key = "HKLM:\SOFTWARE\WOW6432Node\DEXIS\DEXIS"
        $filePath = (Get-ItemProperty -Path $key -Name Directory).Directory + "\dexis.ini"

        # Rather than replace using regex '.*' feature which removes the line and causes issues,
        # I am renaming the beginning line so I can find it, append the correct path to the next line,
        # and then remove the original changed line.

        <# This is a workaround for a bug I found #>
        ((Get-Content -Path $filePath -Raw) -replace 'xdata=', 'D4=') | Set-Content -Path $filePath
    
        (Get-Content $filePath) |
        ForEach-Object {
            $_
            if ($_ -match "D4=.*") {

                "xdata=$dataPath"
            }
        } | Set-Content $filePath

        ((Get-Content -Path $filePath -Raw) -replace 'D4=.*', '') | Set-Content -Path $filePath
        #Write-Host $dataPath " has been set as the new path."
        # Pause
    }
    else {

        $key = "HKLM:\SOFTWARE\DEXIS\DEXIS"
        $filePath = (Get-ItemProperty -Path $key -Name Directory).Directory + "\dexis.ini"
        #$newPath = Read-Host -Prompt "What is the new path of the data? `n"

        ((Get-Content -Path $filePath -Raw) -replace 'xdata=', 'D4=') | Set-Content -Path $filePath
    
        (Get-Content $filePath) |
        ForEach-Object {
            $_
            if ($_ -match "D4=.*") {

                "xdata=$newPath"
            }
        } | Set-Content $filePath

        ((Get-Content -Path $filePath -Raw) -replace 'D4=.*', '') | Set-Content -Path $filePath
        
        #Write-Host $newPath " has been set as the new path."

        # Pause
    }

}

function software_ten() {

    # For this version of the proprietary software, the ini file is in a fixed location,
    # regardless of where the software is installed

    $filePath = "C:\ProgramData\Danaher_Dental\dexis.ini"

    <# Workaround for bug I mentioned earlier #>
    ((Get-Content -Path $filePath -Raw) -replace 'DataPath=', 'D4=') | Set-Content -Path $filePath
    
    (Get-Content $filePath) |
    ForEach-Object {
        $_
        if ($_ -match "D4=.*") {
                
            "DataPath=$dataPath"
        }
    } | Set-Content $filePath

    ((Get-Content -Path $filePath -Raw) -replace 'D4=.*', '') | Set-Content -Path $filePath

    # Write-Host $dataPath " has been set as the new path."
}

# This gets the current location of the script file as it is run
# If the file is put in the data path, the user doesn't need to type in the path manually
# $fileLocation = split-path -parent $MyInvocation.MyCommand.Definition

# $version = Read-Host -Prompt "What version of software have you migrated? `n
# 1) DEXIS 9 `n
# 2) DEXIS 10`n"

switch ($o) {
    1 { software_nine }
    2 { software_ten }
    Default { }
}

# if ([INT16]$version -eq 1) {
#     Clear-Host
#     Write-Host "DEXIS 9"
#     $isManual = Read-Host -Prompt "Is $fileLocation the current server directory? (y/N) `n"

#     if ($isManual.ToLower() -eq "y") {
#         software_nine($fileLocation)
#     }
#     elseif ($isManual.ToLower() -eq "n") {
#         $newPath = Read-Host -Prompt "What is the new path of the data? `n"

#         software_nine($newPath)
#     }
# }
# elseif ([INT16]$version -eq 2) {
#     Write-Host "DEXIS 10"
#     $isManual = Read-Host -Prompt "Is $fileLocation the current server directory? (y/N) `n"

#     if ($isManual.ToLower() -eq "y") {
#         software_ten($fileLocation)
#     }
#     elseif ($isManual.ToLower() -eq "n") {
#         $newPath = Read-Host -Prompt "What is the new path of the data? `n"

#         software_ten($newPath)
#     }