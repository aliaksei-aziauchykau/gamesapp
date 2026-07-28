using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Composition;

public static class GamesAppMicroservicesExtensions
{
    public static IServiceCollection AddGamesAppMicroservices(this IServiceCollection services)
    {
        foreach (var microservice in GamesAppMicroservices.All)
            microservice.RegisterServices(services);

        return services;
    }

    public static WebApplication MapGamesAppMicroservices(this WebApplication app)
    {
        foreach (var microservice in GamesAppMicroservices.All)
            microservice.MapEndpoints(app);

        return app;
    }
}
