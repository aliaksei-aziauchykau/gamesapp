using BGP.Shared.Libs.Microservice.BuildConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Microservice;

public abstract class MicroserviceBuilder : IMicroserviceBuilder
{
    protected IMicroservice? _microservice = null;

    protected IServiceCollection serviceCollection = new ServiceCollection();

    public IMicroservice Microservice => _microservice ?? throw new ArgumentNullException();


    public IMicroserviceBuilder AddServiceCollection(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;

        return this;
    }

    public abstract IMicroserviceBuilder Build(IMicroserviceBuildConfig config);

    public virtual IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        return this;
    }

}
