

using BGP.Shared.Modules.EntityApi;
using Microsoft.Extensions.DependencyInjection;

public class EntityApiClientModule
{
    private IServiceCollection services;

    public static EntityApiClientModule Create()
    {
        return new EntityApiClientModule();
    }

    public EntityApiClientModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EntityApiClientModule AddEntityApiClient(Action<IEntityApiClientConfig> builder)
    {
        var config = new EntityApiClientConfig();

        builder?.Invoke(config);

        services.AddSingleton<IEntityApiClientConfig>(config);

        services.AddScoped(typeof(IEntityApiClient<,>), typeof(EntityApiClient<,>));

        return this;
    }
}
