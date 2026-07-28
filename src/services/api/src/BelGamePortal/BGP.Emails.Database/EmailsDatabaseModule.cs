using System.Reflection;
using BGP.Shared.Libs.Database.DbContextInitializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BGP.Emails.Database;

public class EmailsDatabaseModule
{
    private IServiceCollection services;

    public static EmailsDatabaseModule Create()
    {
        return new EmailsDatabaseModule();
    }

    public EmailsDatabaseModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EmailsDatabaseModule AddSqlDbContext<DbContextType>(IConfiguration configuration, Assembly migrationAssembly, string connectionString = "DefaultConnection") where DbContextType : DbContext
    {
        services.AddDbContext<DbContextType>(options =>
        {

            options.UseSqlServer(configuration[$"Emails:ConnectionStrings:{connectionString}"],
                builder => builder.MigrationsAssembly(migrationAssembly.FullName));

            options.EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
        });

        return this;
    }

    public EmailsDatabaseModule AddMigrateAndSeedServices()
    {
        services.AddScoped<IDbContextInitializer, EmailsDbContextInitializer>();
        services.AddScoped<IDbContextSeeder, EmailsDbContextSeeder>();

        return this;
    }


    public static async Task MigrateAndSeedAsync(IServiceProvider services)
    {
        await DbContextInitializerFacade.InitializeAsync(services);
    }
}
