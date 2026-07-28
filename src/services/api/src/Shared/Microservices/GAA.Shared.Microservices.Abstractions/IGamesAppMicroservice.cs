using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Shared.Microservices.Abstractions;

/// <summary>
/// Composable slice of the GamesApp API surface, mirroring BelGamePortal modular monolith boundaries
/// (Games, Auth, Emails, Identity, Platform).
/// </summary>
public interface IGamesAppMicroservice
{
    /// <summary>Logical name for logging and discovery (e.g. "Games").</summary>
    string Name { get; }

    void RegisterServices(IServiceCollection services);

    void MapEndpoints(IEndpointRouteBuilder endpoints);
}
