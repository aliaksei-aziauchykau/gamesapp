
using BGP.Shared.Libs.Common;
using Hangfire;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.Hangfire;

public class HangfireModule : DIModule<HangfireModule>
{
    public HangfireModule AddHangfireServices(string connectionString, Action<IGlobalConfiguration, IServiceProvider> configure)
    {
        Services.AddHangfire((serviceProvider, configuration) =>
        {
            configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString);

            configure(configuration, serviceProvider);
        });

        return this;
    }

    public HangfireModule AddHangfireServer()
    {
        Services.AddHangfireServer();

        return this;
    }

    public HangfireModule AddHangfireDashboardEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapHangfireDashboard();

        return this;
    }
}
