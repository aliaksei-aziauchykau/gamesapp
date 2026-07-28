using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.EntityApi;

public static class EntityApiConfigureServices
{
    public static IServiceCollection AddEntityApiClient(this IServiceCollection services, Action<IEntityApiClientConfig> builder)
    {
        var config = new EntityApiClientConfig();

        builder?.Invoke(config);

        services.AddSingleton<IEntityApiClientConfig>(config);

        services.AddScoped(typeof(IEntityApiClient<,>), typeof(EntityApiClient<,>));

        return services;
    }
}
