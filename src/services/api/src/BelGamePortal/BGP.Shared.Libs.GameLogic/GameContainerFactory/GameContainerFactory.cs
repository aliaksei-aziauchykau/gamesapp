using System.Diagnostics.CodeAnalysis;

namespace BGP.Shared.Libs.GameLogic;

public class GameContainerFactory : IGameContainerFactory
{
    public GameContainerFactory(IEnumerable<IGameContainerCreator> gameContainerCreators)
    {
        foreach (var gameContainerCreator in gameContainerCreators)
        {
            gameFactories.Add(gameContainerCreator.Name, gameContainerCreator.Create);
        }
    }

    private readonly IDictionary<string, IGameContainer> gameContainers = new Dictionary<string, IGameContainer>();

    private readonly IDictionary<string, Func<string, IGameContainer>> gameFactories = new Dictionary<string, Func<string, IGameContainer>>();

    public void Create(string gameDef, string gameId)
    {
        if (gameContainers.ContainsKey(gameId)) return;

        var factoryMethod = gameFactories[gameDef];
        var gameContainer = factoryMethod.Invoke(gameId);
        gameContainers.Add(gameId, gameContainer);
    }

    public void Destroy(string gameId)
    {
        if (!TryGetValue(gameId, out var gameContainer)) return;

        gameContainers.Remove(gameId);
        gameContainer.Destroy();
    }

    public bool TryGetValue(string gameId, [MaybeNullWhen(false)] out IGameContainer gameServiceToReturn)
    {
        if (gameContainers.TryGetValue(gameId, out var gameService))
        {
            gameServiceToReturn = gameService;
            return true;
        }

        gameServiceToReturn = null;
        return false;
    }

    public string GetListOfContainers()
    {
        return string.Join(", ", gameContainers.Keys.ToArray());
    }
}
