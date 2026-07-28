




using BGP.Emails.Integration.Consumers.Platform;
using BGP.Shared.Modules.ServiceBus;
using MassTransit;

namespace BGP.Emails.Integration;

public class PlatformServiceBusConfigurationProvider : ServiceBusConfigurationProvider
{
    public override void ConfigureBusConfigurator(IBusRegistrationConfigurator config)
    {
        config.AddConsumer<EmailSentIntegrationEventConsumer>();
    }
}
