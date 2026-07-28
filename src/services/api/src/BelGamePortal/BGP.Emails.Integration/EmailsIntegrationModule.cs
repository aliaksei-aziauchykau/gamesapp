using System.Reflection;
using BGP.Emails.Integration;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Modules.Mediator;
using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Emails.Application;

public class EmailsIntegrationModule
{
    private IServiceCollection services;

    public static EmailsIntegrationModule Create()
    {
        return new EmailsIntegrationModule();
    }

    public EmailsIntegrationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EmailsIntegrationModule AddServiceBus(IServiceBusCredsConfiguration creds)
    {
        ServiceBusModule
            .Create()
            .AddServiceCollection(services)
            .AddServiceBus(creds)
            .AddServiceBusConfigProvider<EmailsServiceBusConfigurationProvider>()
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
