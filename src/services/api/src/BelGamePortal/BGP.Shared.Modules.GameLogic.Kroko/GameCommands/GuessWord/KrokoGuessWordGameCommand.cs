using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoGuessWordGameCommand : GameCommand, IRequest
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string Message { get; set; }
}

public class KrokoGuessWordGameCommandHandler : IRequestHandler<KrokoGuessWordGameCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoGuessWordGameCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public async Task Handle(KrokoGuessWordGameCommand command, CancellationToken cancellationToken)
    {
        var gameId = _gameContext.GameId;

        var player = _gameContext.GetPlayerContextByUserId(command.UserId);

        if (player == null) return;

        _gameContext.Messages.Add(new KrokoMessage
        {
            UserId = command.UserId,
            PlayerName = player.Nickname,
            Message = command.Message
        });


        if (_gameContext.CurrentRound.WordToGuess == command.Message.Trim())
        {
            _gameContext.CurrentRound.Winner = player;
            _gameContext.CurrentRound.WinnerUserId = player.UserId;
        }

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);
    }
}
