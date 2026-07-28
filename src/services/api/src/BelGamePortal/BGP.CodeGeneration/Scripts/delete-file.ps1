param (
    [Parameter(Mandatory = $true)]
    [string]$Path
)

try {
    Remove-Item -Path $Path -Recurse -Force
    Write-Host "File ensured for path: $Path" -ForegroundColor Green
}
catch {
    Write-Error "Failed to delete file for path: $Path. Error: $_"
}