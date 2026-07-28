using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands.JoinPlayer;

public class KrokoJoinPlayerGameCommand : GameCommand, IRequest, IWithConnectionId
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string PlayerName { get; set; }

    public string ConnectionId { get; set; }
}

public class KrokoJoinPlayerGameCommandHandler : IRequestHandler<KrokoJoinPlayerGameCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoJoinPlayerGameCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public async Task Handle(KrokoJoinPlayerGameCommand command, CancellationToken cancellationToken)
    {
        var gameId = _gameContext.GameId;

        var player = _gameContext.GetPlayerContextByUserId(command.UserId);

        if (player == null)
        {
            player = new KrokoPlayerContext
            {
                UserId = command.UserId,
                Nickname = command.PlayerName,
            };

            _gameContext.PlayersDict.Add(command.UserId, player);
        }

        //if (_gameContext.CurrentRound.DrawerUserId == null || _gameContext.Players.Count == 1)
        //{
        //    _gameContext.CurrentRound.SetDrawerUserId(command.UserId);
        //}

        await _krokoGameHubContext.Groups.AddToGroupAsync(command.ConnectionId, gameId, cancellationToken);
        await _krokoGameHubContext.Groups.AddToGroupAsync(command.ConnectionId, $"{gameId}-{command.UserId}", cancellationToken);
        player.ConnectionIds.Add(command.ConnectionId);

        await _krokoGameHubContext
            .Clients
            .Groups(new[] { gameId })
            .SendAsync("GameContext", _gameContext, cancellationToken);
    }
}
