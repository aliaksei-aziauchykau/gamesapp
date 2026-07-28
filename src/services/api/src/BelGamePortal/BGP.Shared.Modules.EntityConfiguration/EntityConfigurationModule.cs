using System.Reflection;
using Autofac;
using BGP.Shared.Libs.Database.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.EntityConfiguration;

public class EntityConfigurationModule
{
    private IServiceCollection services;

    public static EntityConfigurationModule Create()
    {
        return new EntityConfigurationModule();
    }

    public EntityConfigurationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EntityConfigurationModule AddEntityConfigurations(Assembly assembly)
    {
        RegisterEntityTypeConfigurations(services, assembly);

        return this;
    }

    private void RegisterEntityTypeConfigurations(IServiceCollection services, Assembly infrastructureAssembly)
    {
        var entityTypeConfigurationDependencies = infrastructureAssembly.DefinedTypes
            .Where(type => !type.IsAbstract
                    && !type.IsGenericTypeDefinition
                    && typeof(EntityTypeConfigurationDependency).IsAssignableFrom(type));

        foreach (var type in entityTypeConfigurationDependencies)
        {
            services.AddSingleton(typeof(EntityTypeConfigurationDependency));
        }
    }

}
