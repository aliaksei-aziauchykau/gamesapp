using GAA.Application.Games;
using GAA.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddGamesAppInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IGamesReadRepository, GamesReadRepository>();
        return services;
    }
}
