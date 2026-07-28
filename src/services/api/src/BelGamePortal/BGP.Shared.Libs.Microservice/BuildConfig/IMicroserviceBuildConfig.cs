

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BGP.Shared.Libs.Microservice.BuildConfig;

public interface IMicroserviceBuildConfig
{
    public IHostEnvironment Environment { get; init; }

    public IConfigurationManager ConfigurationManager { get; init; }

    public IServiceCollection ServiceCollection { get; init; }
}
