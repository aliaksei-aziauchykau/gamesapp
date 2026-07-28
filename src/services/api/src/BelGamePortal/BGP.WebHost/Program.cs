using BGP.Auth.Microservice;
using BGP.Emails.Microservice;
using BGP.Games.Domain.Entities;
using BGP.Games.Microservice;
using BGP.Identity.Microservice;
using BGP.Platform.Microservice;
using BGP.Shared.Libs.Api.Extensions;
using BGP.Shared.Libs.Api.Middlewares;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Shared.Modules.Swagger.Pipelines;
using BGP.WebHost;

using Microsoft.AspNetCore.OData;
using Serilog;

Log.Logger = new LoggerConfiguration()
     .Enrich.FromLogContext()
     .WriteTo.Console()
     .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDotNetEnvVariables(builder.Environment.EnvironmentName);


var buildConfig = new MicroserviceBuildConfig(builder.Environment, builder.Configuration, builder.Services);
var microservicesKeeper = MicroservicesKeeper.Create()
    .AddMicroservices(
        EmailsMicroserviceBuilder
            .Create()
            .Build(buildConfig)
            .Register(builder.Services)
            .Microservice
     )
    .AddMicroservices(
        GamesMicroserviceBuilder
            .Create()
            .Build(buildConfig)
            .Register(builder.Services)
            .Microservice
     )
    .AddMicroservices(
        IdentityMicroserviceBuilder
            .Create()
            .Build(buildConfig)
            .Register(builder.Services)
            .Microservice
    )
    .AddMicroservices(
        AuthMicroserviceBuilder
            .Create()
            .Build(buildConfig)
            .Register(builder.Services)
            .Microservice
    )
    .AddMicroservices(
        PlatformMicroserviceBuilder
            .Create()
            .Build(buildConfig)
            .Register(builder.Services)
            .Microservice
    );

builder.AddWebHostServices(microservicesKeeper);

var app = builder.Build();

var a = app.Configuration;

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSerilogRequestLogging();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseODataRouteDebug();
    app.UseSwaggerPipeline(app.Services);
}

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();

app.UseStaticFiles();
// app.UseIdentityModuleStaticFiles(app.Environment);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CamelToPascalCaseMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    microservicesKeeper.RouteEndpoints(endpoints);
    endpoints.LogEndpointsToConsole();
});

await microservicesKeeper.RunAllAsync();


app.Run();




