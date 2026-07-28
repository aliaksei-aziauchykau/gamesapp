using BGP.Shared.Libs.GameLogic;

namespace BGP.Shared.Modules.GameLogic.Kroko.Restorer;

public class KrokoGameRestorer : IGameRestorer
{
    private readonly IGameStateRestorer _gameStateRestorer;
    private readonly IGameLogic _gameLogic;

    public KrokoGameRestorer(IGameStateRestorer gameStateRestorer, IGameLogic gameLogic)
    {
        _gameStateRestorer = gameStateRestorer;
        _gameLogic = gameLogic;
    }

    public async Task RestoreAsync(IGameContext gameContext)
    {
        var gameState = _gameStateRestorer.Restore(gameContext);

        await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = gameState });
    }
}
