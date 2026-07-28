using BGP.Emails.Application;
using BGP.Emails.Database;
using BGP.Emails.Microservice.Configuration;
using BGP.Emails.Microservice.Interfaces;
using BGP.Emails.OData.Api;
using BGP.Emails.OData.Endpoints;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Shared.Modules.CrudCommands;
using BGP.Shared.Modules.DotNetEnv;
using BGP.Shared.Modules.Email;
using BGP.Shared.Modules.MapCommands;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Emails.Microservice;

public class EmailsMicroserviceBuilder : MicroserviceBuilder
{
    public static IMicroserviceBuilder Create()
    {
        return new EmailsMicroserviceBuilder();
    }

    public override IMicroserviceBuilder Build(IMicroserviceBuildConfig buildConfig)
    {
        var configurationManager = buildConfig.ConfigurationManager;
        var rootServiceCollection = buildConfig.ServiceCollection;
        var environment = buildConfig.Environment;
        var services = this.serviceCollection;

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(configurationManager, environment, "../BGP.Emails.Microservice/", "Emails");

        var emailsMicroserviceConfiguration = configurationManager.GetEmailsMicroserviceConfiguration();

        MapCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddMappings(EmailsODataEndpointsAssemblyHelper.GetAssembly())
            .AddMapCommands(EmailsODataEndpointsAssemblyHelper.GetAssembly());

        CrudCommandsModule
            .Create()
            .AddServiceCollection(services)
            .AddGenericServices()
            .AddDbContextSource(typeof(EmailDbContext))
            .AddCrudCommands(EmailsDomainAssemblyHelper.GetAssembly());

        EmailModule.Create()
            .AddServiceCollection(services)
            .AddServices(emailsMicroserviceConfiguration.EmailSender);

        EmailsApplicationModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator();

        EmailsIntegrationModule
            .Create()
            .AddServiceCollection(services)
            .AddServiceBus(emailsMicroserviceConfiguration.ServiceBusCreds);

        EmailsDatabaseModule
            .Create()
            .AddServiceCollection(services)
            .AddSqlDbContext<EmailDbContext>(configurationManager, EmailsDatabaseAssemblyHelper.GetAssembly())
            .AddMigrateAndSeedServices();

        EmailsODataEndpointsModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddEndpoints(configurationManager, environment);

        var serviceProvider = this.serviceCollection.BuildServiceProvider();

        this._microservice = new EmailsMicroservice(serviceProvider);

        return this;
    }

    public override IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        rootServiceCollection.AddSingleton(typeof(IEmailsMicroservice), this.Microservice);

        return this;
    }
}
