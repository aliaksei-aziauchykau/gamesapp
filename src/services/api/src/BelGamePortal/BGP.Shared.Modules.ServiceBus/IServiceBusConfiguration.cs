using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public interface IServiceBusConfiguration
{
    Action<IBusRegistrationConfigurator>? BusConfigurator { get; set; }

    Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? RabbitMqConfigurator { get; set; }
}
