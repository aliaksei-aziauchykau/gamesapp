using System.Reflection;
using BGP.Emails.Integration;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Modules.Mediator;
using BGP.Shared.Modules.ServiceBus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Emails.Application;

public class PlatformIntegrationModule
{
    private IServiceCollection services;

    public static PlatformIntegrationModule Create()
    {
        return new PlatformIntegrationModule();
    }

    public PlatformIntegrationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public PlatformIntegrationModule AddServiceBus(IServiceBusCredsConfiguration creds)
    {
        ServiceBusModule
            .Create()
            .AddServiceCollection(services)
            .AddServiceBus(creds)
            .AddServiceBusConfigProvider<PlatformServiceBusConfigurationProvider>()
            .WithDefaultMediatorConsumers(Assembly.GetExecutingAssembly());

        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(Assembly.GetExecutingAssembly());

        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(Assembly.GetExecutingAssembly())
            .AddMapCommands(Assembly.GetExecutingAssembly());

        return this;
    }
}
