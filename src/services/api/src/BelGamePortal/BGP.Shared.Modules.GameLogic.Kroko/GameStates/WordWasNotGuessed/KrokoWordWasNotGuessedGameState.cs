using BGP.Shared.Libs.GameLogic;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoWordWasNotGuessedGameState : GameState, IKrokoWordWasNotGuessedGameState
{
    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;
    //private readonly IKrokoLoadNewRoundGuessedGameState _krokoLoadNewRoundGuessedGameState;

    public KrokoWordWasNotGuessedGameState(IMediator mediator, IGameLogic gameLogic)
    {
        _mediator = mediator;
        _gameLogic = gameLogic;
        //_krokoLoadNewRoundGuessedGameState = krokoLoadNewRoundGuessedGameState;
    }

    public override async Task OnEntryAsync()
    {
        Thread.Sleep(5000);

        await _gameLogic.ChangeGameStateToAsync<IKrokoLoadNewRoundGuessedGameState>();
    }
}
