namespace BGP.Shared.Libs.GameLogic.Interfaces;

public interface IGameHubContext
{
    Task SendToGameGroupAsync(string gameId, string method, CancellationToken cancellationToken = default);

    Task SendToGameGroupAsync<T>(string gameId, string method, T paylod, CancellationToken cancellationToken = default);

    Task SendToUserAsync(string gameId, string userId, string method, CancellationToken cancellationToken = default);

    Task SendToUserAsync<T>(string gameId, string userId, string method, T payload, CancellationToken cancellationToken = default);

    Task AddConnectionToGameGroupAsync(string connectionId, string gameId, CancellationToken cancellationToken = default);

    Task AddConnectionToUserGroupAsync(string connectionId, string gameId, string userId, CancellationToken cancellationToken = default);

    Task RemoveConnectionFromGameGroupAsync(string connectionId, string gameId, CancellationToken cancellationToken = default);

    Task RemoveConnectionFromUserGroupAsync(string connectionId, string gameId, string userId, CancellationToken cancellationToken = default);
}
