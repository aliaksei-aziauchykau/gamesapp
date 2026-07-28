using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Common;

public static class RegisterEntitiesExtensions
{
    public static IServiceCollection RegisterAsService(this IServiceCollection services, Assembly assembly, Type interf, Action<IServiceCollection, Type, Type> register)
    {
        var implementations = assembly
            .GetTypes()
            .Where(type => RegisterEntitiesHelper.HasInterface(type, interf))
            .ToList();

        foreach (var implementation in implementations)
        {
            register(services, interf, implementation);
        }

        return services;
    }

    public static IServiceCollection RegisterAsScopedServices(this IServiceCollection services, Assembly assembly, Type interf)
    {

        return RegisterAsService(
            services,
            assembly,
            interf,
            (services, interf, implementation) => services.AddScoped(interf, implementation));
    }

    public static IServiceCollection RegisterAsSingletonServices(this IServiceCollection services, Assembly assembly, Type interf)
    {

        return RegisterAsService(
            services,
            assembly,
            interf,
            (services, interf, implementation) => services.AddSingleton(interf, implementation));
    }
}
