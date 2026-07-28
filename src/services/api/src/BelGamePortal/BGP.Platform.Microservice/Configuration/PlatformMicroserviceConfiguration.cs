

using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace BGP.Platform.Microservice.Configuration;

public class PlatformMicroserviceConfiguration
{
    public required ServiceBusCredsConfiguration ServiceBusCreds { get; init; }
}

public static class PlatformMicroserviceConfigurationExtensions
{
    public static PlatformMicroserviceConfiguration GetPlatformMicroserviceConfiguration(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Platform").Get<PlatformMicroserviceConfiguration>() ?? throw new Exception("Platform configuration is missing");

        return config;
    }
}

public class ServiceBusCredsConfiguration : IServiceBusCredsConfiguration
{
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
