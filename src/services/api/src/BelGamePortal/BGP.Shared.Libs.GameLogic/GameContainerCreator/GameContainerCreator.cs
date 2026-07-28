using MediatR;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace BGP.Shared.Libs.GameLogic;

public abstract class GameContainerCreator : IGameContainerCreator
{
    public abstract string Name { get; }

    public abstract IGameContainer Create(string gameId);

    private IUnityContainer _unityContainer;

    public IUnityContainer UnityContainer => _unityContainer;

    public virtual GameContainerCreator CreateContainer()
    {
        this._unityContainer = new UnityContainer();

        return this;
    }

    public virtual GameContainerCreator AddMediator()
    {
        var containerFactory = new ServiceProviderFactory(null);
        var serviceProvider = containerFactory.CreateServiceProvider(UnityContainer);
        UnityContainer.RegisterFactory<IMediator>(f => new Mediator(serviceProvider));
        return this;
    }

    public virtual GameContainerCreator AddBasicCommands()
    {
        this.UnityContainer.RegisterType<IRequestHandler<GameContainerConnectCommand>, GameContainerConnectCommandHandler>();
        this.UnityContainer.RegisterType<IRequestHandler<GameContainerDisconnectCommand>, GameContainerDisconnectCommandHandler>();
        return this;
    }
}
