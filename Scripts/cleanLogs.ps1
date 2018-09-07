$logs = "Logs"
$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition

foreach($log in Get-ChildItem -Path "$scriptPath/../$logs"){
    Remove-Item -Path $log.FullName -Force -Recurse
}