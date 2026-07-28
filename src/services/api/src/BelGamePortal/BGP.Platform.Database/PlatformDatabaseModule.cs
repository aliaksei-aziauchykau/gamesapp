using System.Reflection;
using BGP.Shared.Libs.Database.DbContextInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BGP.Shared.Libs.Common;

namespace BGP.Platform.Database;

public class PlatformDatabaseModule
{
    private IServiceCollection services;

    public static PlatformDatabaseModule Create()
    {
        return new PlatformDatabaseModule();
    }

    public PlatformDatabaseModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public PlatformDatabaseModule AddSqlDbContext<DbContextType>(IConfiguration configuration, Assembly migrationAssembly, string connectionString = "DefaultConnection") where DbContextType : DbContext
    {
        services.AddDbContext<DbContextType>(options =>
        {
            options.UseSqlServer(configuration[$"Platform:ConnectionStrings:{connectionString}"],
                builder =>
                {
                    builder.MigrationsAssembly(migrationAssembly.FullName);
                    builder.MigrationsHistoryTable("__EFMigrationsHistory", "platform");
                });

            options.EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
        });

        return this;
    }

    public PlatformDatabaseModule AddMigrateAndSeedServices()
    {
        services.AddScoped<IDbContextInitializer, PlatformDbContextInitializer>();
        services.RegisterAsScopedServices(Assembly.GetExecutingAssembly(), typeof(IDbContextSeeder));

        return this;
    }

    public static async Task MigrateAndSeedAsync(IServiceProvider services)
    {
        await DbContextInitializerFacade.InitializeAsync(services);
    }
}
