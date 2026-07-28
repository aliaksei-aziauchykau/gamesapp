using System.Reflection;
using BGP.Platform.Application.Modules.Posts;
using BGP.Platform.Domain;
using BGP.Platform.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using BGP.Shared.Modules.EntityConfiguration;
using BGP.Shared.Modules.Mediator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Platform.Application;

public class PlatformApplicationModule
{
    private IServiceCollection services;

    public static PlatformApplicationModule Create()
    {
        return new PlatformApplicationModule();
    }

    public PlatformApplicationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public PlatformApplicationModule AddModuleServices()
    {
        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(Assembly.GetExecutingAssembly());

        FormEntitySchemasModule
           .Create()
           .AddServiceCollection(services)
           // need to fix this
           .AddCommands(PlatformDomainAssemblyHelper.GetAssembly())
           .AddProviders(Assembly.GetExecutingAssembly());

        // services.AddScoped<ICrudService<Post, Guid>, PostsCrudService>();
        // services.AddScoped

        return this;
    }

}
