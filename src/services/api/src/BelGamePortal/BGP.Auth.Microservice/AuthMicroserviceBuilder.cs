using BGP.Auth.Application;
using BGP.Auth.Domain;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Modules.CrudCommands;
using BGP.Shared.Modules.Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BGP.Auth.OData.Endpoints;
using BGP.Auth.Microservice.Interfaces;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Auth.Application.Modules.AuthApiManagement;
using BGP.Shared.Modules.DotNetEnv;
using BGP.Shared.Modules.CachePipeline;

namespace BGP.Auth.Microservice;

public class AuthMicroserviceBuilder : MicroserviceBuilder
{
    public static IMicroserviceBuilder Create()
    {
        return new AuthMicroserviceBuilder();
    }

    public override IMicroserviceBuilder Build(IMicroserviceBuildConfig buildConfig)
    {
        var configurationManager = buildConfig.ConfigurationManager;
        var rootServiceCollection = buildConfig.ServiceCollection;
        var environment = buildConfig.Environment;
        var services = this.serviceCollection;

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(configurationManager, environment, "../BGP.Auth.Microservice", "auth");

        AuthApplicationModule
            .Create()
            .AddServiceCollection(services)
            .AddModules()
            .AddManagementApiConfig(this.GetApiManagementConfig(configurationManager));

        AuthODataEndpointsModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddEndpoints(configurationManager, environment);

        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(AuthODataEndpointsAssemblyHelper.GetAssembly())
            .AddMapCommands(AuthODataEndpointsAssemblyHelper.GetAssembly());

        var serviceProvider = this.serviceCollection.BuildServiceProvider();

        this._microservice = new AuthMicroservice(serviceProvider);

        return this;
    }

    public override IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        rootServiceCollection.AddSingleton(typeof(IAuthMicroservice), this.Microservice);

        return this;
    }

    private AuthManagementApiConfig GetApiManagementConfig(IConfiguration configuration)
    {
        var apiManagementConfig = configuration.GetSection("Auth:ManagementApi").Get<AuthManagementApiConfig>()
            ?? throw new Exception("AuthManagementApiConfig is not configured");

        return apiManagementConfig;
    }
}
