$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

Set-Location -Path $scriptDir

dotnet build