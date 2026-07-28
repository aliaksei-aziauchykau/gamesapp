using MediatR;
using Unity;

namespace BGP.Shared.Libs.GameLogic;

public interface IGameContainer
{
    IUnityContainer UnityContainer { get; }

    IGamePerformer Performer { get; }

    IGameLogic GameLogic { get; }

    IGameContext GameContext { get; }

    IGameSettings GameSettings { get; }

    IGameContainerState GetState();

    IMediator Mediator { get; }

    void Start();

    Task InitializeAsync();

    void Stop();

    void Destroy();
}
