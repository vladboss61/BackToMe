$scriptPath = Split-Path -parent $MyInvocation.MyCommand.Definition
$Launch = "BackToMe";
$BackEnd = "$scriptPath\.."
$ClientApp = "$scriptPath\..\ClientApp"
$defaultPeriodOfTimeToUpBackEnd = 80

[int]$typeOfDeploy = Read-Host "1-Back-End`n2-Client`n3-Both of"
switch ($typeOfDeploy) {
     1 {         
         & dotnet.exe run --project $BackEnd --launch-profile $Launch 
        }
     2 { 
         Set-Location -Path $ClientApp 
         ng serve --serve-path $ClientApp }
     3 {
        Write-Host "Path to back-end [dotnet.exe ..args..] -> $BackEnd" 
        Start-Process -FilePath "dotnet.exe" -ArgumentList "run --project $BackEnd --launch-profile $Launch "  -WorkingDirectory $PSScriptRoot 
        Write-Host "Back-end is upped" -ForegroundColor Green;
        
        Start-Sleep -Seconds $defaultPeriodOfTimeToUpBackEnd
        
        Write-Host "Path to client [ng serve] -> $ClientApp"
        
        Set-Location -Path $ClientApp #ng dont have a arg that serve angular application by path, only from current folder 
        ng serve
        Write-Host "Angular client is upped" -ForegroundColor Green; 
    }
    Default {
        Write-Host "Error mode of deployment" -ForegroundColor Red
    }
}

