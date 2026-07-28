using GAA.Application.Games.Handlers;
using GAA.Application.Status.Handlers;
using GAA.Infrastructure;
using GAA.Microservices.Composition;
using GAA.Shared.Modules.Auth.Implementation;
using GAA.Shared.Modules.Auth.Interfaces;
using GAA.Shared.Modules.Common.Implementation;
using GAA.Shared.Modules.Mediator.Implementation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.WithThreadId()
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
        .CreateLogger();

    Log.Information("Starting GamesApp API");
    builder.Host.UseSerilog();

    var corsOrigins = (Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS")
        ?? builder.Configuration["Cors:AllowedOrigins"]
        ?? "http://localhost:4200")
        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowUI", policy =>
        {
            policy.WithOrigins(corsOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "GamesApp API",
            Version = "1.0.0",
            Description = "Consolidated API for the Games application (Keycloak + PostgreSQL + observability stack)."
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT from Keycloak"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        });
    });

    builder.Services.AddHealthChecks();

    CommonModule.Create(builder.Services).AddCommon();

    builder.Services.AddGamesAppInfrastructure();

    MediatorModule.Create(builder.Services)
        .AddMediator(typeof(GetStatusQueryHandler).Assembly);

    var authority = Environment.GetEnvironmentVariable("KEYCLOAK_AUTHORITY")
        ?? builder.Configuration["Keycloak:Authority"]
        ?? "http://localhost:8180/realms/gamesapp";

    var audience = Environment.GetEnvironmentVariable("KEYCLOAK_AUDIENCE")
        ?? builder.Configuration["Keycloak:Audience"];

    var requireHttpsMetadata = bool.TryParse(
        Environment.GetEnvironmentVariable("KEYCLOAK_REQUIRE_HTTPS_METADATA"),
        out var httpsMeta)
        ? httpsMeta
        : builder.Configuration.GetValue("Keycloak:RequireHttpsMetadata", false);

    KeycloakAuthModule.Create(builder.Services)
        .AddKeycloakJwtBearer(new KeycloakAuthConfiguration
        {
            Authority = authority,
            Audience = audience,
            RequireHttpsMetadata = requireHttpsMetadata
        });

    builder.Services.AddGamesAppMicroservices();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSerilogRequestLogging();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamesApp API v1"));

    app.UseRouting();
    app.UseCors("AllowUI");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapGamesAppMicroservices();
    app.MapHealthChecks("/health/ready", new HealthCheckOptions
    {
        Predicate = _ => true
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "GamesApp API stopped unexpectedly");
    throw;
}
finally
{
    await Log.CloseAndFlushAsync();
}
