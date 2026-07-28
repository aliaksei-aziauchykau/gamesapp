
using System.ComponentModel;
using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Hubs;
using BGP.Shared.Modules.GameLogic.Hubs.Contexts;
using BGP.Shared.Modules.GameLogic.Kroko.ContainerCreator;
using BGP.Shared.Modules.Hangfire;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.GameLogic;

public class GameLogicModule : DIModule<GameLogicModule>
{
    public GameLogicModule AddGameServices()
    {
        Services.AddSingleton<IGameContainerFactory, GameContainerFactory>();
        Services.AddSingleton<IGameContainerFacade, GameContainerFacade>();

        return this;
    }

    public GameLogicModule AddHangfireModule(string connectionString)
    {
        Services.AddSingleton<JobActivator, CustomJobActivator>();

        HangfireModule
            .Create(Services)
            .AddHangfireServer()
            .AddHangfireServices(
                connectionString,
                (configuration, serviceProvider) =>
            {
                var jobActivator = new CustomJobActivator(serviceProvider.GetRequiredService<IGameContainerFactory>());

                configuration.UseActivator(jobActivator);
            });

        return this;
    }

    public GameLogicModule AddGameContainerHubContext()
    {
        Services.AddSingleton<IGameContainerHubContext, GameContainerHubContext>();

        return this;
    }

    public GameLogicModule AddGameContainerHubEndpoint(IEndpointRouteBuilder endpoints, string url = "/gameContainerHub")
    {
        endpoints.MapHub<GameContainerHub>(url);

        return this;
    }
}
