param (
    [Parameter(Mandatory = $true)]
    [string]$Path
)

try {
    New-Item -Path $Path -ItemType Directory -Force | Out-Null
    Write-Host "Directory structure ensured for path: $Path" -ForegroundColor Green
}
catch {
    Write-Error "Failed to create directory structure for path: $Path. Error: $_"
}