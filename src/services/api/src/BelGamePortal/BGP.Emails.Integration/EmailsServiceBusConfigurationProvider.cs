




using BGP.Emails.Integration.Consumers.Platform;
using BGP.Shared.Modules.ServiceBus;
using MassTransit;

namespace BGP.Emails.Integration;

public class EmailsServiceBusConfigurationProvider : ServiceBusConfigurationProvider
{
    public override void ConfigureBusConfigurator(IBusRegistrationConfigurator config)
    {
        config.AddConsumer<PostCreatedIntegrationEventConsumer>();
    }

    public override void ConfigureRabbitMqConfigurator(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator config)
    {
        // config.ReceiveEndpoint("post-created-integration-event", e =>
        // {
        //     e.ConfigureConsumer<PostCreatedIntegrationEventConsumer>(context);
        // });
    }
}
