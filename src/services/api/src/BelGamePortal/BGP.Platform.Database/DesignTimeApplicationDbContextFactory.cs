using System.Diagnostics;
using System.Reflection;
using BGP.Shared.Modules.DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BGP.Platform.Database;
public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<PlatformDbContext>
{
    public PlatformDbContext CreateDbContext(string[] args)
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
            .ReadEnvFiles(config, "Design", "../BGP.Platform.Microservice/", "Platform");

        var configRoot = ((IConfigurationBuilder)config).Build();

        var connectionString = configRoot["Platform:ConnectionStrings:DefaultConnection"];

        var options = new DbContextOptionsBuilder<PlatformDbContext>();
        options.UseSqlServer(
            connectionString,
            builder =>
            {
                builder.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                builder.MigrationsHistoryTable("__EFMigrationsHistory", "platform");
            });

        return new PlatformDbContext(options.Options, null);
    }
}
