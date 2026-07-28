# Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process

$scriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path
$scriptToImport = "Variables.ps1"
$importScriptPath = Join-Path -Path $scriptDirectory -ChildPath $scriptToImport
. $importScriptPath

Set-Location $RootLocation

dotnet ef database update `
    --startup-project $StartUpPoject `
    --project $Project `
    --context $DbContext
