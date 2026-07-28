using BGP.Games.Database;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Modules.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko;
using BGP.Shared.Modules.Hangfire;
using Hangfire;
using Microsoft.AspNetCore.Routing;

namespace BGP.Games.Microservice;

public class GamesMicroservice(IServiceProvider serviceProvider) : BaseMicroservice(serviceProvider ?? throw new Exception()), IGamesMicroservice
{
    public override void RouteEndpoints(IEndpointRouteBuilder endpoints)
    {
        GameLogicModule
            .Create()
            .AddGameContainerHubEndpoint(endpoints);

        KrokoGameModule
            .Create()
            .AddKrokoGameHubEndpoint(endpoints);

        HangfireModule
            .Create()
            .AddHangfireDashboardEndpoint(endpoints);
    }

    public override async Task RunAsync()
    {
        await GamesDatabaseModule.MigrateAndSeedAsync(serviceProvider);
    }
}
