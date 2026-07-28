using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoDrawWordGameCommand : GameCommand, IRequest
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string ShapesJson { get; set; }
}

public class KrokoDrawWordGameCommandHandler : IRequestHandler<KrokoDrawWordGameCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoDrawWordGameCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public async Task Handle(KrokoDrawWordGameCommand command, CancellationToken cancellationToken)
    {
        var gameId = _gameContext.GameId;

        var player = _gameContext.GetPlayerContextByUserId(command.UserId);

        if (player == null) return;

        _gameContext.CurrentRound.ShapesJson = command.ShapesJson;

        await _krokoGameHubContext.Clients.Groups(new[] { gameId })
            .SendAsync("GameContext", _gameContext, cancellationToken);
    }
}
