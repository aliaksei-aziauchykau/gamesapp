using System.Reflection;
using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using BGP.Shared.Modules.FormEntitySchemas.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.EntityConfiguration;

public class FormEntitySchemasModule
{
    private IServiceCollection services;

    public static FormEntitySchemasModule Create()
    {
        return new FormEntitySchemasModule();
    }

    public FormEntitySchemasModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public FormEntitySchemasModule AddDefaultServices()
    {
        services.AddScoped(typeof(IFormEntitySchemaConfiguration<>), typeof(NullFormEntitySchemaProvider<>));

        return this;
    }

    public FormEntitySchemasModule AddCommands(Assembly domainAssembly)
    {
        services.RegisterFormSchemaCommands(domainAssembly);

        return this;
    }

    public FormEntitySchemasModule AddProviders(Assembly domainAssembly)
    {
        services.RegisterFormSchemaProviders(domainAssembly);

        return this;
    }
}
