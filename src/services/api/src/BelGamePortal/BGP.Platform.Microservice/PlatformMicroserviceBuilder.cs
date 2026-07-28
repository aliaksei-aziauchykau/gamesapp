using BGP.Emails.Application;
using BGP.Platform.Application;
using BGP.Platform.Database;
using BGP.Platform.Domain;
using BGP.Platform.Microservice.Configuration;
using BGP.Platform.Microservice.Interfaces;
using BGP.Platform.OData.Endpoints;
using BGP.Platform.ODataEndpoints;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Shared.Modules.CrudCommands;
using BGP.Shared.Modules.DotNetEnv;
using BGP.Shared.Modules.EntityConfiguration;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Modules.Mediator;
using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BGP.Platform.Microservice;

public class PlatformMicroserviceBuilder : MicroserviceBuilder
{
    public static IMicroserviceBuilder Create()
    {
        return new PlatformMicroserviceBuilder();
    }

    public override IMicroserviceBuilder Build(IMicroserviceBuildConfig buildConfig)
    {
        var configurationManager = buildConfig.ConfigurationManager;
        var rootServiceCollection = buildConfig.ServiceCollection;
        var environment = buildConfig.Environment;
        var services = this.serviceCollection;

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(configurationManager, environment, "../BGP.Platform.Microservice/", "Platform");

        var platformMicroserviceConfiguration = configurationManager.GetPlatformMicroserviceConfiguration();

        // TODO Remove unused modules or reconfig
        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(PlatformApplicationAssemblyHelper.GetAssembly());

        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(PlatformODataEndpointsAssemblyHelper.GetAssembly())
            .AddMapCommands(PlatformODataEndpointsAssemblyHelper.GetAssembly());

        CrudCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddGenericServices()
            .AddDbContextSource(typeof(PlatformDbContext))
            .AddCrudCommands(PlatformDomainAssemblyHelper.GetAssembly());

        PlatformApplicationModule
            .Create()
            .AddServiceCollection(services)
            .AddModuleServices();

        PlatformIntegrationModule
            .Create()
            .AddServiceCollection(services)
            .AddServiceBus(platformMicroserviceConfiguration.ServiceBusCreds);

        var defaultConnection = configurationManager["Platform:ConnectionStrings:DefaultConnection"]
            ?? throw new InvalidOperationException("Connection string not found");

        PlatformDatabaseModule
            .Create()
            .AddServiceCollection(services)
            .AddSqlDbContext<PlatformDbContext>(configurationManager, PlatformDatabaseAssemblyHelper.GetAssembly())
            .AddMigrateAndSeedServices();

        PlatformODataEndpointsModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddEndpoints(configurationManager, environment);

        var serviceProvider = this.serviceCollection.BuildServiceProvider();

        this._microservice = new PlatformMicroservice(serviceProvider);

        return this;
    }

    public override IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        rootServiceCollection.AddSingleton(typeof(IPlatformMicroservice), this.Microservice);

        return this;
    }
}
