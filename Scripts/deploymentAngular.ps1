$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition
$deployPath = "$scriptPath/.."
& dotnet.exe run  --project $deployPath --launch-profile BackToMe