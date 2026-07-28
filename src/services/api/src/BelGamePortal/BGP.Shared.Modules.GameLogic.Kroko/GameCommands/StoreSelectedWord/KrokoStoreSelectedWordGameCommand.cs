using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoStoreSelectedWordGameCommand : GameCommand, IRequest
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string WordSelected { get; set; }
}

public class KrokoStoreSelectedWordGameCommandHandler : IRequestHandler<KrokoStoreSelectedWordGameCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoStoreSelectedWordGameCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public Task Handle(KrokoStoreSelectedWordGameCommand command, CancellationToken cancellationToken)
    {
        _gameContext.CurrentRound.WordToGuess = command.WordSelected;

        return Task.CompletedTask;
    }
}
