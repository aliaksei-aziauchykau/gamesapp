using System.Diagnostics;
using System.Reflection;
using BGP.Shared.Modules.DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BGP.Games.Database;
public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<GamesDbContext>
{
    public GamesDbContext CreateDbContext(string[] args)
    {

        // Debug Instructions Below
        // Log.Information($"Current ProcessID: {Process.GetCurrentProcess().Id}");

        // while (!Debugger.IsAttached)
        // {
        //     Thread.Sleep(100);
        // }

        var config = new ConfigurationManager();

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(config, "Design", "../BGP.Games.Microservice/", "Games");

        var configRoot = ((IConfigurationBuilder)config).Build();

        var connectionString = configRoot["Games:ConnectionStrings:DefaultConnection"];

        var options = new DbContextOptionsBuilder<GamesDbContext>();
        options.UseSqlServer(
            connectionString,
            builder =>
            {
                builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                builder.MigrationsHistoryTable("__EFMigrationsHistory", "games");
            });

        return new GamesDbContext(options.Options, null);
    }
}
