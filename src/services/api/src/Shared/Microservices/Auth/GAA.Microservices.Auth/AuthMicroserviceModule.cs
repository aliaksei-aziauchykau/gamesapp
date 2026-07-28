using GAA.Shared.Microservices.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Auth;

public sealed class AuthMicroserviceModule : IGamesAppMicroservice
{
    public string Name => "Auth";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/auth").WithTags("Auth");
        group.MapGet("/info", () => Results.Ok(new
        {
            module = "Auth",
            note = "Interactive login is handled by Keycloak (OIDC). This boundary matches BelGamePortal BGP.Auth.Microservice for future policy and token-adjacent APIs."
        })).AllowAnonymous();
    }
}
