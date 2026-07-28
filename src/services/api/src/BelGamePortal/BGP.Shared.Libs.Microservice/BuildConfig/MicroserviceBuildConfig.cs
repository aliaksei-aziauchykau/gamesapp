

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BGP.Shared.Libs.Microservice.BuildConfig;

public record MicroserviceBuildConfig(
    IHostEnvironment Environment,
    IConfigurationManager ConfigurationManager,
    IServiceCollection ServiceCollection
) : IMicroserviceBuildConfig
{
}
