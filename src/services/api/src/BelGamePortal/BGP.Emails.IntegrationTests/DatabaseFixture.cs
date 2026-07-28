

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

public class DatabaseFixture<TDBContext> : IDisposable where TDBContext : DbContext
{
    public TDBContext Context { get; private set; }
    private readonly SqliteConnection _connection;

    public DatabaseFixture()
    {
        // Create in-memory SQLite database
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<TDBContext>()
            .UseSqlite(_connection)
            .Options;

        Context = CreateContext(options);

        // Ensure the database is created
        Context.Database.EnsureCreated();
    }

    protected virtual object?[] GetContextConstructorParams()
    {
        return [null, "default"];
    }

    protected virtual TDBContext CreateContext(DbContextOptions<TDBContext> options)
    {
        var parameters = new object?[] { options };
        var constructorParameters = parameters.Concat(this.GetContextConstructorParams()).ToArray();
        var context = Activator.CreateInstance(typeof(TDBContext), constructorParameters)
            ?? throw new XunitException($"Could not create instance of {typeof(TDBContext).Name}");

        var contextSafe = context is TDBContext dbContext
            ? dbContext
            : throw new XunitException($"Could not cast instance of {typeof(TDBContext).Name} to {typeof(TDBContext).Name}");

        return contextSafe;
    }

    public void Dispose()
    {
        Context?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}
