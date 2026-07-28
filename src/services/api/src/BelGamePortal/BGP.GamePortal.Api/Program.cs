using BGP.GamePortal.Api;
using BGP.Games.Microservice;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using BGP.Shared.Modules.Swagger.Pipelines;
using BGP.Shared.Libs.Api.Middlewares;
using Microsoft.AspNetCore.OData;
using Serilog;
using BGP.Shared.Libs.Api.Extensions;

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
        GamesMicroserviceBuilder
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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CamelToPascalCaseMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    microservicesKeeper.RouteEndpoints(endpoints);
    endpoints.LogEndpointsToConsole();
});

await microservicesKeeper.RunAllAsync();

app.Run();




