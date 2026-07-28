

using System.Reflection;
using BGP.Games.Application;
using BGP.Games.Database;
using BGP.Games.Domain;
using BGP.Games.Microservice.Configuration;
using BGP.Games.Microservice.Interfaces;
using BGP.Games.OData.Endpoints;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Shared.Modules.CrudCommands;
using BGP.Shared.Modules.DotNetEnv;
using BGP.Shared.Modules.EntityConfiguration;
using BGP.Shared.Modules.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko;
using BGP.Shared.Modules.GameLogic.Kroko.Interfaces;
using BGP.Shared.Modules.MapCommands;
using BGP.Shared.Modules.Mediator;
using BGP.Shared.Modules.ServiceBus;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Games.Microservice;
public class GamesMicroserviceBuilder : MicroserviceBuilder
{
    public static IMicroserviceBuilder Create()
    {
        return new GamesMicroserviceBuilder();
    }

    public override IMicroserviceBuilder Build(IMicroserviceBuildConfig buildConfig)
    {
        var configurationManager = buildConfig.ConfigurationManager;
        var rootServiceCollection = buildConfig.ServiceCollection;
        var environment = buildConfig.Environment;
        var services = this.serviceCollection;

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(configurationManager, environment, "../BGP.Games.Microservice/", "Games");

        var gamesMicroserviceConfiguration = configurationManager.GetGamesMicroserviceConfiguration();

        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(GamesApplicationAssemblyHelper.GetAssembly());

        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(GamesODataEndpointsAssemblyHelper.GetAssembly(), Assembly.GetExecutingAssembly())
            .AddMapCommands(GamesODataEndpointsAssemblyHelper.GetAssembly(), Assembly.GetExecutingAssembly());

        CrudCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddGenericServices()
            .AddDbContextSource(typeof(GamesDbContext))
            .AddCrudCommands(GamesDomainAssemblyHelper.GetAssembly());

        FormEntitySchemasModule
            .Create()
            .AddServiceCollection(services)
            .AddCommands(GamesDomainAssemblyHelper.GetAssembly())
            .AddProviders(GamesApplicationAssemblyHelper.GetAssembly());

        ServiceBusModule
            .Create()
            .AddServiceCollection(services)
            .AddServiceBus(gamesMicroserviceConfiguration.ServiceBusCreds);

        GamesApplicationModule
            .Create()
            .AddServiceCollection(services)
            .AddModuleServices();

        GamesDatabaseModule
            .Create()
            .AddServiceCollection(services)
            .AddSqlDbContext<GamesDbContext>(configurationManager, GamesDatabaseAssemblyHelper.GetAssembly())
            .AddMigrateAndSeedServices();

        GamesODataEndpointsModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddEndpoints(configurationManager, environment);

        var hangfireConnectionString = configurationManager["Games:ConnectionStrings:Hangfire"]
             ?? throw new InvalidOperationException("Connection string not found");

        GameLogicModule
            .Create(rootServiceCollection)
            .AddHangfireModule(hangfireConnectionString)
            .AddGameContainerHubContext()
            .AddGameServices();

        services.AddScoped<IKrokoGameDataService, KrokoGameDataService>();

        KrokoGameModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddKrokoGameContainerCreator()
            .AddKrokoGameHubContext();

        var serviceProvider = this.serviceCollection.BuildServiceProvider();

        this._microservice = new GamesMicroservice(serviceProvider);

        return this;
    }

    public override IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        rootServiceCollection.AddSingleton(typeof(IGamesMicroservice), this.Microservice);

        return this;
    }
}
