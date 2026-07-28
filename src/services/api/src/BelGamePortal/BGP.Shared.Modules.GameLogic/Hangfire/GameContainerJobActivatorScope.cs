using BGP.Shared.Libs.GameLogic;
using Hangfire;
using Unity;

namespace BGP.Shared.Modules.GameLogic;

public class GameContainerJobActivatorScope : JobActivatorScope
{
    private readonly IGameContainerFactory _gameContainerFactory;

    private readonly string _gameId;

    public GameContainerJobActivatorScope(IGameContainerFactory gameContainerFactory, string gameId)
    {
        _gameContainerFactory = gameContainerFactory;
        _gameId = gameId;
    }

    public override object Resolve(Type type)
    {
        this._gameContainerFactory.TryGetValue(_gameId, out var gameContainer);

        if (gameContainer == null)
            throw new NullReferenceException($"Can activate job - there is no container with id {_gameId}. There are: [{this._gameContainerFactory.GetListOfContainers()}]");

        return gameContainer.UnityContainer.Resolve(type);
    }
}
