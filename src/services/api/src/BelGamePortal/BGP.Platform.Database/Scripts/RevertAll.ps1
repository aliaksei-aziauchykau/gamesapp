# Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process

$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
$scriptToImport = "Variables.ps1"
$importScriptPath = Join-Path -Path $scriptDirectory -ChildPath $scriptToImport
. $importScriptPath

Set-Location $RootLocation

dotnet ef database update 0 `
    --startup-project $StartUpPoject `
    --project $Project `
    --context $DbContext

# dotnet ef database update 0 --startup-project BGP.WebHost --project BGP.Platform.Database --context PlatformDbContext