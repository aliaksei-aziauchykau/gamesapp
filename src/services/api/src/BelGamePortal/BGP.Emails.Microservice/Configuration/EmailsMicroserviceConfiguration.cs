

using BGP.Shared.Modules.Email;
using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace BGP.Emails.Microservice.Configuration;

public class EmailsMicroserviceConfiguration
{
    public required EmailConfiguration EmailSender { get; init; }

    public required ServiceBusCredsConfiguration ServiceBusCreds { get; init; }
}

public static class EmailsMicroserviceConfigurationExtensions
{
    public static EmailsMicroserviceConfiguration GetEmailsMicroserviceConfiguration(this IConfiguration configuration)
    {
        var config = configuration.GetSection("Emails").Get<EmailsMicroserviceConfiguration>() ?? throw new Exception("Emails configuration is missing");

        return config;
    }
}
