$logs = "Logs"

$InvokedStriptFolderPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
foreach($log in (Get-ChildItem -Path "$InvokedStriptFolderPath/../$logs")){
	$path = $log.FullName
	Write-Host -Path $path  
    Remove-Item -Path $path -Force -Recurse
}