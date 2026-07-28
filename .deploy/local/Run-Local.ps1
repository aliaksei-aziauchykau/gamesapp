# One-click local deploy from GamesApp repo root:
#   .\.deploy\local\Run-Local.ps1 -IncludeDevServices up -d --build
param(
    [switch]$IncludeDevServices,
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]]$Rest
)
$core = Join-Path (Split-Path -Parent $PSScriptRoot) "Deploy-GamesApp.ps1"
if ($IncludeDevServices) {
    & $core -DeployConfigDirectory $PSScriptRoot -IncludeDevServices @Rest
} else {
    & $core -DeployConfigDirectory $PSScriptRoot @Rest
}
exit $LASTEXITCODE
