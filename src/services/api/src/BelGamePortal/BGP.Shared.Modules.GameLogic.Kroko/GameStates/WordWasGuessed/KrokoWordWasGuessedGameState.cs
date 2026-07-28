using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoWordWasGuessedGameState : GameState, IKrokoWordWasGuessedGameState
{
    private readonly IKrokoGameContext _gameContext;

    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoWordWasGuessedGameState(
        IMediator mediator,
        IGameLogic gameLogic,
        IKrokoGameHubContext krokoGameHubContext,
        IKrokoGameContext gameContext)
    {
        _mediator = mediator;
        _gameLogic = gameLogic;
        _krokoGameHubContext = krokoGameHubContext;
        _gameContext = gameContext;
    }

    public override async Task OnEntryAsync()
    {


        Thread.Sleep(5000);

        await _gameLogic.ChangeGameStateToAsync<IKrokoLoadNewRoundGuessedGameState>();
    }
}
