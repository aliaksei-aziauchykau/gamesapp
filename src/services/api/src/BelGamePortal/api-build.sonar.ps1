$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

$parentDir = Split-Path -Path $scriptDir -Parent
$parentDir = Split-Path -Path $parentDir -Parent
$parentDir = Split-Path -Path $parentDir -Parent
$rootDir = Split-Path -Path $parentDir -Parent

# Change to the parent directory
Set-Location -Path $rootDir

# Load Helpers
Import-Module -Name "$rootDir/automate/scripts/helpers/utils.psm1"

Set-Location -Path $scriptDir

$SonarProjectToken = GetEnvValue -envFilePath "$scriptDir/.env" -key "SONARQUBE_TOKEN"
$SonarProjectKey = GetEnvValue -envFilePath "$scriptDir/.env" -key "SONARQUBE_PROJECTKEY"
$SonarUrl = GetEnvValue -envFilePath "$scriptDir/.env" -key "SONARQUBE_URL"
# dotnet tool install --global dotnet-sonarscanner

dotnet sonarscanner begin /k:$SonarProjectKey /d:sonar.host.url=$SonarUrl /d:sonar.login=$SonarProjectToken 

dotnet build --no-incremental

dotnet sonarscanner end /d:sonar.login=$SonarProjectToken 