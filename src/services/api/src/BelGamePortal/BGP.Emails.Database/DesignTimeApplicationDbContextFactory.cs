using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BGP.Emails.Database;
public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<EmailDbContext>
{
    public EmailDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddDotNetEnvVariables(
                environmentName: "Development",
                basePath: Path.Combine(Directory.GetCurrentDirectory(), "../BGP.Emails.OData.Endpoints"),
                prefix: "Emails")
            .Build();

        var options = new DbContextOptionsBuilder<EmailDbContext>();
        options.UseSqlServer(
            config["Emails:ConnectionStrings:DefaultConnection"],
            builder => builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

        return new EmailDbContext(options.Options, null);
    }
}
