param (
    [string]$ProjectName = "MyLibrary", # Default project name
    [string]$Directory = "."       # Default output directory (current directory)
)

# Validate dotnet is installed
if (-not (Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host "Error: .NET SDK is not installed or 'dotnet' is not in your PATH."
    exit 1
}

# Define the full path where the project will be created
$projectPath = $Directory

# Create the directory if it does not exist
if (-not (Test-Path $projectPath)) {
    New-Item -Path $projectPath -ItemType Directory | Out-Null
}

# Navigate to the project directory
Set-Location $projectPath 

# Create a new class library project using dotnet CLI
try {
    dotnet new classlib -n $ProjectName
    Write-Host "Successfully created class library project: $ProjectName"
}
catch {
    Write-Host "Error occurred while creating the project: $_"
}
