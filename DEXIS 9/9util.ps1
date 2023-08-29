$filePath = "C:\DEXIS\Dexis.ini"
if ((Get-Content -Path $filePath -Raw) -contains 'maxvisible.*') {
    ((Get-Content -Path $filePath -Raw) -replace 'maxvisible.*', 'maxvisible=18') | Set-Content -Path $filePath
}
else {
    ForEach-Object {
        $_
        if ($_ -match "[SOFTWARE]") {

            "maxvisible=18"
        }
    } | Set-Content $filePath
}