using Microsoft.AspNetCore.Routing;

namespace BGP.Shared.Libs.Microservice;

public interface IMicroservicesKeeper
{
    IMicroservicesKeeper AddMicroservices(IMicroservice installer);

    Task RunAllAsync();

    void RouteEndpoints(IEndpointRouteBuilder endpoints);

    IEnumerable<T> GetServices<T>();
}
