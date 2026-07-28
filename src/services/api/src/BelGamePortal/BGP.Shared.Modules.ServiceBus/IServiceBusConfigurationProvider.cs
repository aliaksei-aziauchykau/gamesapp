using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public interface IServiceBusConfigurationProvider
{
    IServiceBusConfiguration ServiceBusConfig { get; }
}
