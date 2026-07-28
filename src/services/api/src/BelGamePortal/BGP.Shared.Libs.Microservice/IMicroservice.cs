using Microsoft.AspNetCore.Routing;

namespace BGP.Shared.Libs.Microservice;

public interface IMicroservice : IProvideServiceProvider
{
    void RouteEndpoints(IEndpointRouteBuilder endpoints);

    Task RunAsync();

    Task StopAsync();
}
