$logs = "Logs"

Write-Host $MyInvocation.MyCommand.Definition
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

foreach($log in Get-ChildItem -Path "$scriptPath/../$logs"){
	$path = $log.FullName
	Write-Host -Path $path 
    Remove-Item -Path $path -Force -Recurse
}