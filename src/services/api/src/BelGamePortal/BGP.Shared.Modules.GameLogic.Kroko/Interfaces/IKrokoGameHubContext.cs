using BGP.Shared.Modules.GameLogic.Kroko.Context;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Libs.GameLogic.Interfaces;

public interface IKrokoGameHubContext : IHubContext
{
    Task SendAllGameContextAsync(IKrokoGameContext gameContext, CancellationToken cancellationToken = default);

    Task SendToPlayerAsync(string gameId, string userId, string method);

    Task SendToPlayerAsync<T>(string gameId, string userId, string method, T payload);
}
