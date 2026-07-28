Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process

Set-Location "C:\Root\Projects\VolnyaSiervisy\src\game-portal\backend\BelGamePortal"

$StartUpPoject = "BGP.Games.OData.Api"
$Project = "BGP.Games.Database"
$DbContext = "GamesDbContext"
$OutputDir = "./Migrations"

dotnet ef migrations add Initial `
    --startup-project $StartUpPoject `
    --project $Project `
    --context $DbContext `
    --output-dir $OutputDir `
    --verbose

dotnet ef database update `
    --startup-project $StartUpPoject `
    --project $Project `
    --context $DbContext

# Reverts
dotnet ef database update Initial `
    --startup-project BGP.WebHost `
    --project BGP.Emails.Database `
    --context GamesDbContext

dotnet ef migrations list `
    --startup-project BGP.WebHost `
    --project BGP.Emails.Database `
    --context GamesDbContext
