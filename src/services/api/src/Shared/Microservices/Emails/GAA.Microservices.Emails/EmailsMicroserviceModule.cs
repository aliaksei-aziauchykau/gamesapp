using GAA.Shared.Microservices.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Emails;

public sealed class EmailsMicroserviceModule : IGamesAppMicroservice
{
    public string Name => "Emails";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/emails").WithTags("Emails");
        group.MapGet("/info", () => Results.Ok(new
        {
            module = "Emails",
            note = "Stub — mirrors BelGamePortal BGP.Emails.Microservice; integrate SMTP or a message bus later."
        })).AllowAnonymous();
    }
}
