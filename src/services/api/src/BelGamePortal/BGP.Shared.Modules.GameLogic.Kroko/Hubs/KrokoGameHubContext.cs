using BGP.Shared.Libs.GameLogic.Hubs;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Hubs;

public class KrokoGameHubContext : BaseHubContext<KrokoGameHub>, IKrokoGameHubContext
{
    public KrokoGameHubContext(IHubContext<KrokoGameHub> hubContext) : base(hubContext)
    {
    }

    public async Task SendAllGameContextAsync(IKrokoGameContext gameContext, CancellationToken cancellationToken = default)
    {
        await Clients.Groups(new[] { gameContext.GameId }).SendAsync("GameContext", gameContext, cancellationToken);
    }


    public async Task SendToPlayerAsync(string gameId, string userId, string method)
    {
        await Clients.Groups(new[] { $"{gameId}-{userId}", }).SendAsync(method);
    }

    public async Task SendToPlayerAsync<T>(string gameId, string userId, string method, T payload)
    {
        await Clients.Groups(new[] { $"{gameId}-{userId}", }).SendAsync(method, payload);
    }
}
