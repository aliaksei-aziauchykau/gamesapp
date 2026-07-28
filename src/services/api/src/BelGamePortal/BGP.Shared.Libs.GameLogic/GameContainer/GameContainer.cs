using MediatR;
using Unity;

namespace BGP.Shared.Libs.GameLogic;

public abstract class GameContainer : IGameContainer
{
    private readonly GameContainerState _gameContainerState = new GameContainerState();

    private readonly IUnityContainer _container;

    public GameContainer(IUnityContainer container)
    {
        _container = container;
    }

    public IUnityContainer UnityContainer => _container;

    public IGamePerformer Performer => _container.Resolve<IGamePerformer>();

    public IGameLogic GameLogic => _container.Resolve<IGameLogic>();

    public IGameContext GameContext => _container.Resolve<IGameContext>();

    public IGameSettings GameSettings => _container.Resolve<IGameSettings>();

    public IMediator Mediator => _container.Resolve<IMediator>();

    public virtual void Destroy()
    {
        Performer.Stop();
        UnityContainer.Dispose();
    }

    public virtual void Start()
    {
        Performer.Start();
    }

    public virtual void Stop()
    {
        Performer.Stop();
    }

    public IGameContainerState GetState()
    {
        _gameContainerState.isRunning = Performer.IsRunning;
        _gameContainerState.Created = true;
        _gameContainerState.CommandsQueue = Performer.CommandQueueu;
        _gameContainerState.CurrentGameStateName = GameLogic.CurrentGameStateName;

        return _gameContainerState;
    }

    public virtual Task InitializeAsync()
    {
        _gameContainerState.Initialized = true;

        return Task.CompletedTask;
    }
}
