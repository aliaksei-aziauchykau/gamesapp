using System.Reflection;
using BGP.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BGP.Identity.Database;

public class IdentityDatabaseModule
{
    private IServiceCollection services;

    public static IdentityDatabaseModule Create()
    {
        return new IdentityDatabaseModule();
    }

    public IdentityDatabaseModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public IdentityDatabaseModule AddSqlDbContext<DbContextType>(IConfiguration configuration, Assembly migrationAssembly, string connectionString = "DefaultConnection") where DbContextType : DbContext
    {
        services.AddDbContext<DbContextType>(options =>
        {
            options.UseSqlServer(configuration[$"Identity:ConnectionStrings:{connectionString}"],
                builder => builder.MigrationsAssembly(migrationAssembly.FullName));

            options.EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        });

        services.AddIdentity<User, UserRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<IdentityAppDbContext>()
            .AddDefaultTokenProviders();

        return this;
    }
}
