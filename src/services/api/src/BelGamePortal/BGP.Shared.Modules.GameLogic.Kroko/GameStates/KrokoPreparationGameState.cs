using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.Context;

namespace BGP.Shared.Modules.GameLogic.Kroko.States;

public class KrokoPreparationGameState : GameState, IPreparationGameState
{
    private readonly IKrokoGameContext _gameContext;

    public KrokoPreparationGameState(IKrokoGameContext gameContext)
    {
        _gameContext = gameContext;
    }

    public override Task HandleGameCommandAsync(GameCommand command)
    {
        _gameContext.Layout.ShowBoard();

        return Task.CompletedTask;

    }
}
