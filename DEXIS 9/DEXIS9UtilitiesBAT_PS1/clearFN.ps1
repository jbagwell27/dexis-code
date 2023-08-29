$infFiles = Get-ChildItem . *.inf -rec
    foreach ($file in $infFiles) {
        Write-Host $file
        (Get-Content $file.PSPath) |
        Foreach-Object { $_ -replace "FN=.*", "" } |
        Set-Content $file.PSPath
    }