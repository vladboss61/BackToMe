$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition
$Launch = "BackToMe";
$BackEnd = "$scriptPath\.."
$ClientApp = "$scriptPath\..\ClientApp"

[int]$typeOfDeploy = Read-Host "1-Client 2-Back-End and Client (SPA)"
switch ($typeOfDeploy) {
     1 { 
         Set-Location -Path $ClientApp 
         ng serve 
	}
     2 {
        Write-Host "Path to back-end [dotnet.exe ..args..] -> $BackEnd" 
        & dotnet.exe run --project $BackEnd
        Write-Host "Back-end is upped" -ForegroundColor Green
        Write-Host "Path to client [ng serve] -> $ClientApp"        
        #Set-Location -Path $ClientApp #ng dont have a arg that serve angular application by path, only from current folder 
        #ng serve
        #Write-Host "Angular client is upped" -ForegroundColor Green; 
    }
    Default {
        Write-Host "Error mode of deployment" -ForegroundColor Red
    }
}

