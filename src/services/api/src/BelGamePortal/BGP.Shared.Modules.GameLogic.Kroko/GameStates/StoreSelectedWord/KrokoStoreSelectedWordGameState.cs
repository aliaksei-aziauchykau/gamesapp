using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoStoreSelectedWordGameState : GameState, IKrokoStoreSelectedWordGameState
{
    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;
    private readonly IKrokoDrawGuessWordGameState _krokoDrawGuessWordGameState;

    public KrokoStoreSelectedWordGameState(IMediator mediator, IGameLogic gameLogic, IKrokoDrawGuessWordGameState krokoDrawGuessWordGameState)
    {
        _mediator = mediator;
        _gameLogic = gameLogic;
        _krokoDrawGuessWordGameState = krokoDrawGuessWordGameState;
    }

    protected override IList<Type> AcceptableCommandTypes => new List<Type> {
        typeof(KrokoStoreSelectedWordGameCommand)
    };


    public override async Task HandleGameCommandAsync(GameCommand command)
    {
        await _mediator.Send(command);

        await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = _krokoDrawGuessWordGameState });
    }
}
