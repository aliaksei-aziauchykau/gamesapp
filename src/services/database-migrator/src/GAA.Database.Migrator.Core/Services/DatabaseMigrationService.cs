using DbUp;
using Microsoft.Extensions.Logging;

namespace GAA.Database.Migrator.Core.Services;

public class DatabaseMigrationService
{
    private readonly string _connectionString;
    private readonly string _scriptsPath;
    private readonly string _journalTable;
    private readonly ILogger<DatabaseMigrationService>? _logger;

    public DatabaseMigrationService(
        string connectionString,
        string scriptsPath = "Migrations/Up",
        string journalTable = "SchemaVersions",
        ILogger<DatabaseMigrationService>? logger = null)
    {
        _connectionString = connectionString;
        _scriptsPath = scriptsPath;
        _journalTable = journalTable;
        _logger = logger;
    }

    public bool RunMigrations(bool dryRun = false)
    {
        var scriptsPath = Path.IsPathRooted(_scriptsPath)
            ? _scriptsPath
            : Path.Combine(AppContext.BaseDirectory, _scriptsPath);

        if (!Directory.Exists(scriptsPath))
        {
            _logger?.LogError("Migration scripts directory not found: {Path}", scriptsPath);
            return false;
        }

        var builder = DeployChanges.To
            .PostgresqlDatabase(_connectionString)
            .WithTransaction()
            .JournalToPostgresqlTable("public", _journalTable)
            .WithScriptsFromFileSystem(scriptsPath);

        if (_logger != null)
        {
            builder = builder.LogToConsole();
        }
        else
        {
            builder = builder.LogToConsole();
        }

        var upgrader = builder.Build();

        if (dryRun)
        {
            var scripts = upgrader.GetScriptsToExecute();
            _logger?.LogInformation("DRY RUN: {Count} pending migration(s)", scripts.Count);
            return true;
        }

        var result = upgrader.PerformUpgrade();
        if (result.Successful)
        {
            _logger?.LogInformation("Migrations applied successfully.");
            return true;
        }

        _logger?.LogError("Migration failed: {Error}", result.Error);
        return false;
    }
}
