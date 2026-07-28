
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace BGP.Shared.Modules.Mediator;

public class MediatorModule
{
    private IServiceCollection services;

    public static MediatorModule Create()
    {
        return new MediatorModule();
    }

    public MediatorModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;

    }
    public MediatorModule AddMediator(Assembly assembly)
    {

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assembly);
        });

        return this;
    }
}
