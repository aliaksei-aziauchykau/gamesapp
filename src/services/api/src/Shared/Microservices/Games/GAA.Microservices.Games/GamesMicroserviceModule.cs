using System.Security.Claims;
using GAA.Application.Games;
using GAA.Application.Status;
using GAA.Shared.Microservices.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Microservices.Games;

public sealed class GamesMicroserviceModule : IGamesAppMicroservice
{
    public string Name => "Games";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/games").WithTags("Games");

        group.MapGet("/status", async (IMediator mediator, CancellationToken cancellationToken) =>
        {
            var dto = await mediator.Send(new GetStatusQuery(), cancellationToken);
            return Results.Ok(dto);
        }).AllowAnonymous();

        group.MapGet("/profile", (ClaimsPrincipal user) =>
        {
            var name = user.Identity?.Name
                ?? user.FindFirst("preferred_username")?.Value
                ?? "unknown";
            return Results.Ok(new
            {
                user = name,
                claims = user.Claims.Select(c => new { c.Type, c.Value })
            });
        }).RequireAuthorization();

        group.MapGet("/game-definitions", async (IMediator mediator, CancellationToken ct) =>
            Results.Ok(await mediator.Send(new GetGameDefinitionsQuery(), ct)))
            .AllowAnonymous();

        group.MapGet("/game-definitions/{code}", async (string code, IMediator mediator, CancellationToken ct) =>
        {
            var items = await mediator.Send(new GetGameDefinitionsQuery(), ct);
            var item = items.FirstOrDefault(x =>
                string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
            return item is null ? Results.NotFound() : Results.Ok(item);
        }).AllowAnonymous();

        group.MapGet("/sessions", async (IMediator mediator, CancellationToken ct) =>
            Results.Ok(await mediator.Send(new GetGameSessionsQuery(), ct)))
            .AllowAnonymous();

        group.MapGet("/lobbies", async (IMediator mediator, CancellationToken ct) =>
            Results.Ok(await mediator.Send(new GetGameLobbiesQuery(), ct)))
            .AllowAnonymous();

        // Legacy OData path alias for migrated UI (was /api/v1/GameDefinitions).
        var odata = endpoints.MapGroup("/api/v1").WithTags("Games (OData compatibility)");
        odata.MapGet("/GameDefinitions", async (IMediator mediator, CancellationToken ct) =>
        {
            var items = await mediator.Send(new GetGameDefinitionsQuery(), ct);
            return Results.Ok(new { value = items });
        }).AllowAnonymous();

        odata.MapGet("/Games", async (IMediator mediator, CancellationToken ct) =>
        {
            var items = await mediator.Send(new GetGameSessionsQuery(), ct);
            return Results.Ok(new { value = items });
        }).AllowAnonymous();
    }
}
