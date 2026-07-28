namespace BGP.Shared.Libs.GameLogic;

public abstract class GameContext : IGameContext
{
    private readonly string? _gameId;

    public GameContext()
    {
    }

    public GameContext(string gameId)
    {
        _gameId = gameId;
    }

    public string GameId => _gameId;
}
