using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using BGP.Shared.Modules.GameLogic.Kroko.GameCommands;
using BGP.Shared.Modules.GameLogic.Kroko.GameStates;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.States;

public class KrokoInitialGameState : GameState, IKrokoInitialGameState
{
    private readonly IKrokoGameContext _gameContext;

    private readonly IGameContainerHubContext _hubContext;

    private readonly IGameLogic _gameLogic;

    private readonly IMediator _mediator;

    private readonly Type[] _acceptableCommandTypes =
    [
        typeof(KrokoStartGameCommand),
    ];

    public KrokoInitialGameState(
        IKrokoGameContext gameContext,
        IGameLogic gameLogic,
        IGameContainerHubContext hubContext,
        IMediator mediator,
        IKrokoLoadNewRoundGuessedGameState loadNewRoundGuessedGameState)
    {
        _gameContext = gameContext;
        _hubContext = hubContext;
        _gameLogic = gameLogic;
        _mediator = mediator;
    }

    public override bool isCommandAccepted(GameCommand command)
    {
        return _acceptableCommandTypes.Contains(command.GetType());
    }

    public override async Task OnEntryAsync()
    {
        _gameContext.Layout.ShowBoard();

        await _hubContext.SendToGameGroupAsync(_gameContext.GameId, "GameState", this._gameContext);
    }

    public override async Task HandleGameCommandAsync(GameCommand command)
    {
        // await base.HandleGameCommandAsync(command);
        await _gameLogic.ChangeGameStateToAsync<IKrokoLoadNewRoundGuessedGameState>();
    }
}
