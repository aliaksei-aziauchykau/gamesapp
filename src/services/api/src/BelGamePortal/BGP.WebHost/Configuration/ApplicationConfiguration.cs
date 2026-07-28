

using BGP.Shared.Modules.ServiceBus;

namespace BGP.WebHost.Configuration;

public class ApplicationConfiguration
{
    public required ServiceBusCredsConfiguration ServiceBusCreds { get; init; }
}

public static class ApplicationConfigurationExtensions
{
    public static ApplicationConfiguration GetApplicationConfiguration(this IConfiguration configuration)
    {
        var config = configuration.Get<ApplicationConfiguration>() ?? throw new Exception("Application configuration is missing");

        return config;
    }
}

public class ServiceBusCredsConfiguration : IServiceBusCredsConfiguration
{
    public required string Host { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}
