$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

Set-Location -Path $scriptDir

dotnet run --project BGP.WebHost --launch-profile https