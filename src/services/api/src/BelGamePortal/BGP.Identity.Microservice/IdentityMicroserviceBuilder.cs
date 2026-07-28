using BGP.Identity.Database;
using BGP.Identity.Microservice.Interface;
using BGP.Identity.Pages;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Identity.Microservice;

public class IdentityMicroserviceBuilder : MicroserviceBuilder
{
    public static IMicroserviceBuilder Create()
    {
        return new IdentityMicroserviceBuilder();
    }

    public override IMicroserviceBuilder Build(IMicroserviceBuildConfig buildConfig)
    {
        var configurationManager = buildConfig.ConfigurationManager;
        var rootServiceCollection = buildConfig.ServiceCollection;
        var environment = buildConfig.Environment;
        var services = this.serviceCollection;

        IdentityDatabaseModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddSqlDbContext<IdentityAppDbContext>(configurationManager, IdentityDatabaseAssemblyHelper.GetAssembly());

        IdentityPagesModule
            .Create()
            .AddServiceCollection(rootServiceCollection)
            .AddPages(configurationManager)
            .AddEmailSender();

        var serviceProvider = services.BuildServiceProvider();

        this._microservice = new IdentityMicroservice(serviceProvider);

        return this;
    }

    public override IMicroserviceBuilder Register(IServiceCollection rootServiceCollection)
    {
        rootServiceCollection.AddSingleton(typeof(IIdentityMicroservice), this.Microservice);
        //rootServiceCollection.AddSingleton<IEmailSender>(x => {
        //    var microservice = x.GetRequiredService<IIdentityMicroservice>();
        //    var serviceProvider = microservice.ServiceProvider;
        //    var emailSeneder = serviceProvider.GetRequiredService<IEmailSender>();
        //    return emailSeneder;
        //});

        //rootServiceCollection.AddScoped(x =>
        //{
        //    var microservice = x.GetRequiredService<IIdentityMicroservice>();
        //    var serviceProvider = microservice.ServiceProvider;
        //    var signInManager = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<SignInManager<User>>();
        //    return signInManager;
        //});

        //rootServiceCollection.AddScoped(x =>
        //{
        //    var microservice = x.GetRequiredService<IIdentityMicroservice>();
        //    var serviceProvider = microservice.ServiceProvider;
        //    var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<User>>();
        //    return service;
        //});

        //rootServiceCollection.AddSingleton(x =>
        //{
        //    var microservice = x.GetRequiredService<IIdentityMicroservice>();
        //    var serviceProvider = microservice.ServiceProvider;
        //    var service = serviceProvider.GetRequiredService<ILogger<UserManager<User>>>();
        //    return service;
        //});

        return this;
    }
}

