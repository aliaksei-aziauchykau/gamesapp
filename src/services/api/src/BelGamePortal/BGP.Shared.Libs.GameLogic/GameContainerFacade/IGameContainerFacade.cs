using System.Diagnostics.CodeAnalysis;

namespace BGP.Shared.Libs.GameLogic;

public interface IGameContainerFacade
{
    void Create(string gameDef, string gameId);

    void Start(string gameId);

    Task InitializeAsync(string gameId);

    void Stop(string gameId);

    void Destroy(string gameId);

    void SendGameCommand(string gameId, GameCommand gameCommand);

    Task SendCommandAsync<T>(string gameId, T command);

    IGameContainerState GetState(string gameId);

    IGameContext? GetGameContext(string gameId);

    IGameSettings? GetGameSettings(string gameId);

    string GetListOfContainers();
}
