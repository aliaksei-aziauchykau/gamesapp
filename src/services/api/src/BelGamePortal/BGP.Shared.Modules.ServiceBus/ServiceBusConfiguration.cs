using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public class ServiceBusConfiguration : IServiceBusConfiguration
{

    public Action<IBusRegistrationConfigurator>? BusConfigurator { get; set; }

    public Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? RabbitMqConfigurator { get; set; }

}
