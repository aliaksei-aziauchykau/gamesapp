using Microsoft.AspNetCore.Routing;

namespace BGP.Shared.Libs.Microservice;

public abstract class BaseMicroservice : IMicroservice
{
    protected BaseMicroservice(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public IServiceProvider ServiceProvider { get; }

    public virtual void RouteEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    public virtual Task RunAsync()
    {
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }
}
