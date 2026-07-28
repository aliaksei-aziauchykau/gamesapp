using BGP.Shared.Libs.GameLogic.Hubs;
using BGP.Shared.Libs.GameLogic.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Hubs.Contexts;
public class GameHubContext<THub> : BaseHubContext<THub>, IGameHubContext where THub : Hub
{
    public GameHubContext(IHubContext<THub> hubContext) : base(hubContext)
    {
    }

    public async Task SendToGameGroupAsync(string gameId, string method, CancellationToken cancellationToken = default)
    {
        await Clients.Groups(new[] { gameId }).SendAsync(method, cancellationToken);
    }

    public async Task SendToGameGroupAsync<T>(string gameId, string method, T paylod, CancellationToken cancellationToken = default)
    {
        await Clients.Groups(new[] { gameId }).SendAsync(method, paylod, cancellationToken);
    }

    public async Task SendToUserAsync(string gameId, string userId, string method, CancellationToken cancellationToken = default)
    {
        await Clients.Groups(new[] { $"{gameId}-{userId}", }).SendAsync(method);
    }

    public async Task SendToUserAsync<T>(string gameId, string userId, string method, T payload, CancellationToken cancellationToken = default)
    {
        await Clients.Groups(new[] { $"{gameId}-{userId}", }).SendAsync(method, payload);
    }

    public async Task AddConnectionToGameGroupAsync(string connectionId, string gameId, CancellationToken cancellationToken = default)
    {
        await Groups.AddToGroupAsync(connectionId, gameId, cancellationToken);
    }

    public async Task AddConnectionToUserGroupAsync(string connectionId, string gameId, string userId, CancellationToken cancellationToken = default)
    {
        await Groups.AddToGroupAsync(connectionId, $"{gameId}-{userId}", cancellationToken);
    }

    public async Task RemoveConnectionFromGameGroupAsync(string connectionId, string gameId, CancellationToken cancellationToken = default)
    {
        await Groups.RemoveFromGroupAsync(connectionId, gameId, cancellationToken);
    }

    public async Task RemoveConnectionFromUserGroupAsync(string connectionId, string gameId, string userId, CancellationToken cancellationToken = default)
    {
        await Groups.RemoveFromGroupAsync(connectionId, $"{gameId}-{userId}", cancellationToken);
    }
}

