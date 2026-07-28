


using System.Reflection;
using BGP.Auth.Application.Modules.AuthApiManagement;
using BGP.Auth.Application.Modules.AuthUsers;
using BGP.Auth.Domain;
using BGP.Auth.Domain.Entities;
using BGP.Shared.Modules.CachePipeline;
using BGP.Shared.Modules.CrudCommands;
using BGP.Shared.Modules.CrudCommands.Services;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Modules.Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Auth.Application;


public class AuthApplicationModule
{
    private IServiceCollection? _services = null;

    private IServiceCollection services => _services ?? throw new InvalidOperationException("ServiceCollection is not set");

    public static AuthApplicationModule Create()
    {
        return new AuthApplicationModule();
    }

    public AuthApplicationModule AddServiceCollection(IServiceCollection services)
    {
        this._services = services;

        return this;
    }

    public AuthApplicationModule AddManagementApiConfig(AuthManagementApiConfig config)
    {
        services.AddSingleton(config);

        return this;
    }

    public AuthApplicationModule AddModules()
    {
        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(Assembly.GetExecutingAssembly());

        CrudCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddCrudServices()
            .AddCrudCommands(AuthDomainAssemblyHelper.GetAssembly())
            .OverrideServices((services) =>
            {
                services.AddScoped<ICrudService<AuthUser, Guid>, AuthUsersCrudService>();
            });

        CachePipelineModule
            .Create(services)
            .AddMemoryCache()
            .AddCachePipelineMediator(AuthApplicationAssemblyHelper.GetAssembly());

        return this;
    }
}
