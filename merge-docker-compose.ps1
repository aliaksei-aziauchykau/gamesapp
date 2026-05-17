param(
  [Parameter(ValueFromRemainingArguments = $true)]
  [string[]] $PassArgs = @()
)

$Here = Split-Path -Parent $MyInvocation.MyCommand.Path
& (Join-Path $Here "automation\scripts\docker\merge-docker-compose.ps1") @PassArgs
