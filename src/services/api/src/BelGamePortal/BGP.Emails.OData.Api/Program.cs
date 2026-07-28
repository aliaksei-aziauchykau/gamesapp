using BGP.Emails.Microservice;
using BGP.Shared.Libs.Microservice.BuildConfig;
using Microsoft.AspNetCore.OData;
using Serilog;

Log.Logger = new LoggerConfiguration()
     .Enrich.FromLogContext()
     .WriteTo.Console()
     .CreateBootstrapLogger();

Log.Information("Starting up");

var builder = WebApplication.CreateBuilder(args);

var buildConfig = new MicroserviceBuildConfig(builder.Environment, builder.Configuration, builder.Services);
EmailsMicroserviceBuilder
    .Create()
    .AddServiceCollection(builder.Services)
    .Build(buildConfig)
    .Register(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSerilogRequestLogging();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseODataRouteDebug();
    app.UseStaticFiles();
}

app.UseRouting();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints
        .MapControllers();
});

app.Run();
