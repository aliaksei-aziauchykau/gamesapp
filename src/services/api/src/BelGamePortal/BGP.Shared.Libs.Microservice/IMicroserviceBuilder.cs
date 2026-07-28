using BGP.Shared.Libs.Microservice.BuildConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Microservice;

public interface IMicroserviceBuilder
{
    IMicroservice Microservice { get; }

    IMicroserviceBuilder AddServiceCollection(IServiceCollection serviceCollection);

    IMicroserviceBuilder Build(IMicroserviceBuildConfig config);

    IMicroserviceBuilder Register(IServiceCollection serviceCollection);
}
