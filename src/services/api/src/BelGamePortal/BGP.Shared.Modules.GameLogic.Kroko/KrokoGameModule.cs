
using System.ComponentModel;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.ContainerCreator;
using BGP.Shared.Modules.GameLogic.Kroko.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.GameLogic.Kroko;

public class KrokoGameModule
{
    private IServiceCollection services;

    public static KrokoGameModule Create()
    {
        return new KrokoGameModule();
    }

    public KrokoGameModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;


        return this;
    }

    public KrokoGameModule AddKrokoGameContainerCreator()
    {
        services.AddSingleton<IGameContainerCreator, KrokoGameContainerCreator>();

        return this;
    }

    public KrokoGameModule AddKrokoGameHubContext()
    {
        services.AddSingleton<IKrokoGameHubContext, KrokoGameHubContext>();

        return this;
    }

    public KrokoGameModule AddKrokoGameHubEndpoint(IEndpointRouteBuilder endpoints, string url = "/krokoGameHub")
    {

        endpoints.MapHub<KrokoGameHub>(url);

        return this;
    }

}
