
namespace BGP.Shared.Libs.GameLogic;

public class GameSettings : IGameSettings
{
    private readonly string? _gameId;

    public GameSettings()
    {
    }

    public GameSettings(string gameId)
    {
        _gameId = gameId;
    }

    public string GameId => _gameId;
}
