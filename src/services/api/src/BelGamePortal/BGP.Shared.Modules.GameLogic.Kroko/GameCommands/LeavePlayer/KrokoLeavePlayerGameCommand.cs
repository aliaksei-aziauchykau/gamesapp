using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands.LeavePlayer;

public class KrokoLeavePlayerGameCommand : GameCommand, IRequest, IWithConnectionId
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string ConnectionId { get; set; }
}

public class KrokoLeavePlayerGameCommandHandler : IRequestHandler<KrokoLeavePlayerGameCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoLeavePlayerGameCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public async Task Handle(KrokoLeavePlayerGameCommand command, CancellationToken cancellationToken)
    {
        await RemovePlayerAsync(command.GameId, command.UserId, command.ConnectionId, cancellationToken);

        await _krokoGameHubContext
            .Clients
            .Groups(new[] { command.GameId })
            .SendAsync("GameContext", _gameContext, cancellationToken);
    }

    private async Task RemovePlayerAsync(string gameId, string userId, string connectionId, CancellationToken cancellationToken)
    {
        var playerContext = _gameContext.GetPlayerContextByUserId(userId);

        await _krokoGameHubContext.Groups.RemoveFromGroupAsync(connectionId, gameId, cancellationToken);
        await _krokoGameHubContext.Groups.RemoveFromGroupAsync(connectionId, $"{gameId}-{userId}", cancellationToken);
        playerContext.ConnectionIds.Remove(connectionId);

        if (playerContext.ConnectionIds.Count != 0) return;

        _gameContext.PlayersDict.Remove(userId);

        //if (_gameContext.Players.Count == 0) return;

        //_gameContext.CurrentRound.SetDrawerUserId(_gameContext.Players[0].UserId);
    }
}
