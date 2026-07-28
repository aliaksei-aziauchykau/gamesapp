


using System.Reflection;
using BGP.Shared.Libs.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
namespace BGP.Shared.Modules.Serilog;

public class SerilogModule : DIModule<SerilogModule>
{
    public SerilogModule AddSerilog(IHostBuilder hostBuilder)
    {

        hostBuilder.UseSerilog((ctx, lc) =>
        {
            lc.MinimumLevel.Debug()
                .MinimumLevel.Override(source: "Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override(source: "Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override(source: "Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Override(source: "System", LogEventLevel.Warning)
                .MinimumLevel.Override(source: "Hangfire.Server.ServerHeartbeatProcess", LogEventLevel.Warning)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                theme: AnsiConsoleTheme.Code)
                // Hangfire logs turned off
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("hangfire")))
                .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("Hangfire")))
                .Enrich.FromLogContext();
        }, true);

        return this;
    }
}
