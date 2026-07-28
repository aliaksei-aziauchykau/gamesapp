using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public abstract class ServiceBusConfigurationProvider : IServiceBusConfigurationProvider
{
    protected IServiceBusConfiguration _serviceBusConfig = new ServiceBusConfiguration();

    public IServiceBusConfiguration ServiceBusConfig => new ServiceBusConfiguration
    {
        BusConfigurator = ConfigureBusConfigurator,
        RabbitMqConfigurator = ConfigureRabbitMqConfigurator
    };

    public virtual void ConfigureBusConfigurator(IBusRegistrationConfigurator config)
    {
    }

    public virtual void ConfigureRabbitMqConfigurator(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator config)
    {
    }
}
