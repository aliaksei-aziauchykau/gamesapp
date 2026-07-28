using BGP.GamePortal.Api.Configuration;
using BGP.Shared.Libs.Common.Extensions;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Modules.Authentication;
using BGP.Shared.Modules.Cors;
using BGP.Shared.Modules.OData;
using BGP.Shared.Modules.ODataVersioning;
using BGP.Shared.Modules.Serilog;
using BGP.Shared.Modules.ServiceBus;
using BGP.Shared.Modules.SignalR;
using BGP.Shared.Modules.Swagger;

namespace BGP.GamePortal.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddWebHostServices(this WebApplicationBuilder builder, IMicroservicesKeeper microservicesKeeper)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;
        var hostBuilder = builder.Host;

        var mvcBuilder = services
            .AddControllers();

        services
            .AddProblemDetails()
            .AddDatabaseDeveloperPageExceptionFilter()
            .AddHttpContextAccessor();

        ODataModule
            .Create(services)
            .AddODataServices(mvcBuilder);

        SerilogModule
            .Create(services)
            .AddSerilog(hostBuilder);

        SwaggerModule
            .Create(services)
            .AddSwaggerServices(new SwaggerModuleConfiguration
            {
                TokenUrl = configuration.GetValue<string>("Swagger:TokenUrlEndpoint").ThrowExceptionIfNull()
            });

        AuthenticationModule
            .Create(services)
            .AddAuthentication(new AuthenticationModuleConfiguration
            {
                Authority = configuration.GetValue<string>("Auth:Authority").ThrowExceptionIfNull(),
                Audience = "https://itdgameportalodataapi"
            });

        CorsModule
            .Create(services)
            .AddCorsServices(new CorsModuleConfiguration
            {
                // ClientOrigins = configuration.GetValue<string>("Auth:ClientOriginUrl").ThrowExceptionIfNull()
                ClientOrigins = "https://localhost:5100"
            });

        ODataModule
            .Create(services)
            .AddODataServices(mvcBuilder);

        ODataVersioningModule
            .Create(services)
            .AddODataVersioningServices(mvcBuilder);

        SignalRModule
            .Create(services)
            .AddSignalR();

        var serviceBusConfigs = microservicesKeeper.GetServices<IServiceBusConfigurationProvider>().Select(x => x.ServiceBusConfig);
        var applicationConfiguration = configuration.GetApplicationConfiguration();

        ServiceBusModule
            .Create()
            .AddServiceCollection(services)
            // Uncomment this to connect consumers and producers
            //.AddServiceBus(serviceBusConfigs, applicationConfiguration.ServiceBusCreds);
            .AddServiceBus(applicationConfiguration.ServiceBusCreds);

        return services;
    }
}
