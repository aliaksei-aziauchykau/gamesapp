using GAA.Database.Migrator.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

using var loggerFactory = LoggerFactory.Create(b => b.AddConsole().SetMinimumLevel(LogLevel.Information));
var logger = loggerFactory.CreateLogger<DatabaseMigrationService>();

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
    ?? configuration["Database:ConnectionString"]
    ?? throw new InvalidOperationException("DATABASE_CONNECTION_STRING is required");

var scriptsPath = Environment.GetEnvironmentVariable("MIGRATION_SCRIPTS_PATH")
    ?? configuration["Migration:ScriptsPath"]
    ?? "Migrations/Up";

var journalTable = Environment.GetEnvironmentVariable("MIGRATION_JOURNAL_TABLE")
    ?? configuration["Migration:JournalTable"]
    ?? "SchemaVersions";

var service = new DatabaseMigrationService(connectionString, scriptsPath, journalTable, logger);

var argsList = args.ToList();
if (argsList.Count == 0 || argsList[0] is "up" or "migrate")
{
    var dry = argsList.Contains("--dry-run");
    Environment.Exit(service.RunMigrations(dry) ? 0 : 1);
}

Console.WriteLine("Usage: dotnet run -- up [--dry-run]");
Environment.Exit(1);
