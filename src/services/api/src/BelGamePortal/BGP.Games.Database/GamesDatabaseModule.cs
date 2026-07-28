using System.Reflection;
using BGP.Shared.Libs.Database.DbContextInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BGP.Shared.Libs.Common;

namespace BGP.Games.Database;

public class GamesDatabaseModule
{
    private IServiceCollection services;

    public static GamesDatabaseModule Create()
    {
        return new GamesDatabaseModule();
    }

    public GamesDatabaseModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public GamesDatabaseModule AddSqlDbContext<DbContextType>(IConfiguration configuration, Assembly migrationAssembly, string connectionString = "DefaultConnection") where DbContextType : DbContext
    {
        services.AddDbContext<DbContextType>(options =>
        {
            options.UseSqlServer(configuration[$"Games:ConnectionStrings:{connectionString}"],
                builder =>
                {
                    builder.MigrationsAssembly(migrationAssembly.FullName);
                    builder.MigrationsHistoryTable("__EFMigrationsHistory", "games");
                });

            options.EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
        });

        return this;
    }

    public GamesDatabaseModule AddMigrateAndSeedServices()
    {
        services.AddScoped<IDbContextInitializer, GamesDbContextInitializer>();
        services.RegisterAsScopedServices(Assembly.GetExecutingAssembly(), typeof(IDbContextSeeder));

        return this;
    }


    public static async Task MigrateAndSeedAsync(IServiceProvider services)
    {
        await DbContextInitializerFacade.InitializeAsync(services);
    }
}
