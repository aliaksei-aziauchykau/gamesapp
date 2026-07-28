using System.Security.Claims;
using GAA.Shared.Microservices.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Identity;

public sealed class IdentityMicroserviceModule : IGamesAppMicroservice
{
    public string Name => "Identity";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/identity").WithTags("Identity");
        group.MapGet("/info", () => Results.Ok(new
        {
            module = "Identity",
            note = "Stub — mirrors BelGamePortal BGP.Identity.Microservice. JWT claims are sourced from Keycloak."
        })).AllowAnonymous();

        group.MapGet("/me", (ClaimsPrincipal user) =>
        {
            if (user.Identity?.IsAuthenticated != true)
                return Results.Unauthorized();

            return Results.Ok(new
            {
                user = user.Identity.Name
                    ?? user.FindFirst("preferred_username")?.Value,
                email = user.FindFirst("email")?.Value,
                claims = user.Claims.Select(c => new { c.Type, c.Value })
            });
        }).RequireAuthorization();
    }
}
