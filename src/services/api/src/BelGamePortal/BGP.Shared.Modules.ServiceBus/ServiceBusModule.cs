using System.Reflection;
using BGP.Shared.Modules.MapCommands;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.ServiceBus;

public class ServiceBusModule
{
    private IServiceCollection services;

    public static ServiceBusModule Create()
    {
        return new ServiceBusModule();
    }

    public ServiceBusModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public ServiceBusModule AddServiceBusConfigProvider<T>() where T : IServiceBusConfigurationProvider
    {

        this.services.AddSingleton(typeof(IServiceBusConfigurationProvider), typeof(T));

        return this;
    }


    public ServiceBusModule AddServiceBus(IServiceBusCredsConfiguration creds)
    {
        this.AddServiceBus(Enumerable.Empty<IServiceBusConfiguration>(), creds);

        return this;
    }

    public ServiceBusModule AddServiceBus(IEnumerable<IServiceBusConfiguration> configs, IServiceBusCredsConfiguration creds)
    {
        services.AddMassTransit(x =>
        {
            var busConfigs = configs
                .Select(X => X.BusConfigurator)
                .Where(x => x is not null)
                .Cast<Action<IBusRegistrationConfigurator>>();

            foreach (var config in busConfigs)
            {
                config(x);
            }

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{creds.Host}"), h =>
                    {
                        h.Username(creds.Username);
                        h.Password(creds.Password);
                    });

                    var rabbitBusConfigs = configs
                        .Select(X => X.RabbitMqConfigurator)
                        .Where(x => x is not null)
                        .Cast<Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>>();

                    foreach (var config in rabbitBusConfigs)
                    {
                        config(context, cfg);
                    }

                    cfg.ConfigureEndpoints(context);

                });

            x.AddConfigureEndpointsCallback((name, cfg) =>
            {
                if (cfg is IRabbitMqReceiveEndpointConfigurator rmq)
                    rmq.SetQuorumQueue(3);

                cfg.UseMessageRetry(r => r.Immediate(2));
            });

        });

        return this;
    }

    public ServiceBusModule WithDefaultMediatorConsumers(Assembly assembly)
    {
        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(assembly)
            .AddMapCommands(assembly);

        return this;
    }
}
