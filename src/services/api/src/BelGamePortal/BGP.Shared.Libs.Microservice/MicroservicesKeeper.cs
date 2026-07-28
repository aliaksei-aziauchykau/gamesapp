using Microsoft.AspNetCore.Routing;

namespace BGP.Shared.Libs.Microservice;

public class MicroservicesKeeper : IMicroservicesKeeper
{
    private readonly IList<IMicroservice> microservices = new List<IMicroservice>();

    public IMicroservicesKeeper AddMicroservices(IMicroservice microservice)
    {
        microservices.Add(microservice);

        return this;
    }

    public async Task RunAllAsync()
    {
        foreach (var microservice in microservices)
        {
            await microservice.RunAsync();
        }
    }

    public static IMicroservicesKeeper Create()
    {
        return new MicroservicesKeeper();
    }

    public void RouteEndpoints(IEndpointRouteBuilder endpoints)
    {
        foreach (var microservice in microservices)
        {
            microservice.RouteEndpoints(endpoints);
        }
    }

    public IEnumerable<T> GetServices<T>()
    {
        return microservices.Select(x => x.ServiceProvider.GetService(typeof(T))).Where(x => x is not null).Cast<T>();
    }
}
