param (
    [Parameter(Mandatory = $true)]
    [string]$Path
)

try {
    New-Item -Path $Path -ItemType File -Force
    Write-Host "File ensured for path: $Path" -ForegroundColor Green
}
catch {
    Write-Error "Failed to create file for path: $Path. Error: $_"
}