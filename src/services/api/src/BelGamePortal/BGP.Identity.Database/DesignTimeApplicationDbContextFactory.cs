using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BGP.Identity.Database;
public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<IdentityAppDbContext>
{
    public IdentityAppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../BGP.Identity.Pages"))
            .AddJsonFile("appsettings.identity.json")
            .Build();

        var options = new DbContextOptionsBuilder<IdentityAppDbContext>();
        options.UseSqlServer(
            config["Identity:ConnectionStrings:DefaultConnection"],
            builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

        return new IdentityAppDbContext(options.Options);
    }
}
