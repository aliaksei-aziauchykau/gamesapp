

using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace BGP.Games.Microservice.Configuration;

public class GamesMicroserviceConfiguration
{
    public required ServiceBusCredsConfiguration ServiceBusCreds { get; init; }
}

public static class GamesMicroserviceConfigurationExtensions
{
    public static GamesMicroserviceConfiguration GetGamesMicroserviceConfiguration(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Games").Get<GamesMicroserviceConfiguration>() ?? throw new Exception("Games configuration is missing");

        return config;
    }
}
