$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$scriptDir = Split-Path -Path $scriptDir -Parent
Set-Location -Path $scriptDir

dotnet run --project BGP.GamePortal.Api --launch-profile https