using System.Diagnostics.CodeAnalysis;

namespace BGP.Shared.Libs.GameLogic;

public class GameContainerFacade : IGameContainerFacade
{
    private IGameContainerFactory _gameContainerFactory;

    public GameContainerFacade(IGameContainerFactory gameContainerFactory)
    {
        _gameContainerFactory = gameContainerFactory;
    }

    public string GetListOfContainers()
    {
        return _gameContainerFactory.GetListOfContainers();
    }

    public void Create(string gameDef, string gameId)
    {
        _gameContainerFactory.Create(gameDef, gameId);
    }

    public void Destroy(string gameId)
    {
        _gameContainerFactory.Destroy(gameId);
    }

    public void Start(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return;

        var gameState = GetState(gameId);

        if (gameState.isRunning) return;

        container.Start();
    }

    public async Task InitializeAsync(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return;

        var gameState = GetState(gameId);

        if (gameState.Initialized) return;

        await container.InitializeAsync();
    }

    public void Stop(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return;

        container.Stop();
    }

    public void SendGameCommand(string gameId, GameCommand gameCommand)
    {
        if (!TryGetValue(gameId, out var container)) return;

        container.Performer.SendCommand(gameCommand);
    }

    public async Task SendCommandAsync<T>(string gameId, T gameCommand)
    {
        if (!TryGetValue(gameId, out var container)) return;

        if (gameCommand == null) return;

        await container.Mediator.Send(gameCommand);
    }

    public IGameContainerState GetState(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return new GameContainerState();

        return container.GetState();
    }

    public IGameContext? GetGameContext(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return null;

        return container.GameContext;
    }

    public IGameSettings? GetGameSettings(string gameId)
    {
        if (!TryGetValue(gameId, out var container)) return null;

        return container.GameSettings;
    }

    private bool TryGetValue(string gameId, [MaybeNullWhen(false)] out IGameContainer container)
    {
        if (_gameContainerFactory.TryGetValue(gameId, out var gameContainer))
        {
            container = gameContainer;
            return true;
        }

        container = null;
        return false;
    }
}
