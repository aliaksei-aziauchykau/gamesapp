using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public interface IServiceBusCredsConfiguration
{
    public string Host { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}
