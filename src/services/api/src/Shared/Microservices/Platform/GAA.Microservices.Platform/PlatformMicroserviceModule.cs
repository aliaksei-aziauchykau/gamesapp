using GAA.Shared.Microservices.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Platform;

public sealed class PlatformMicroserviceModule : IGamesAppMicroservice
{
    public string Name => "Platform";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/platform").WithTags("Platform");
        group.MapGet("/info", () => Results.Ok(new { module = "Platform", status = "composed" })).AllowAnonymous();

        group.MapGet("/modules", () => Results.Ok(new
        {
            application = "gamesapp",
            modules = new object[]
            {
                new
                {
                    name = "Games",
                    description = "Catalog, sessions, and game logic (BelGamePortal: BGP.Games.Microservice).",
                    basePath = "/api/games"
                },
                new
                {
                    name = "Auth",
                    description = "Authentication boundaries (BelGamePortal: BGP.Auth.Microservice); OIDC via Keycloak in this stack.",
                    basePath = "/api/auth"
                },
                new
                {
                    name = "Identity",
                    description = "User identity services (BelGamePortal: BGP.Identity.Microservice).",
                    basePath = "/api/identity"
                },
                new
                {
                    name = "Emails",
                    description = "Outbound notifications (BelGamePortal: BGP.Emails.Microservice).",
                    basePath = "/api/emails"
                },
                new
                {
                    name = "Platform",
                    description = "Cross-cutting platform capabilities (BelGamePortal: BGP.Platform.Microservice).",
                    basePath = "/api/platform"
                }
            }
        })).AllowAnonymous();
    }
}
