# Get the current script directory
$scriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

# Get the parent directory
$parentDir = Split-Path -Path $scriptDir -Parent
$rootDir = Split-Path -Path $parentDir -Parent

$StartUpPoject = "BGP.WebHost"
$Project = "BGP.Games.Database"
$DbContext = "GamesDbContext"
$OutputDir = "./Migrations"
$RootLocation = $rootDir