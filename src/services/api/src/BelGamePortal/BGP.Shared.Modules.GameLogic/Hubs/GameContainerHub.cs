
using BGP.Shared.Libs.GameLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Hubs;
public class GameContainerHub : Hub
{
    public void Create(string gameDefName, string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.Create(gameDefName, gameId);
    }

    public void Start(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.Start(gameId);
    }

    public async Task Initialize(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        await gameContainerFacade.InitializeAsync(gameId);
    }

    public void Stop(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.Stop(gameId);
    }

    public void Destroy(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.Destroy(gameId);
    }

    public Task GetState(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        return Clients.Caller.SendAsync("GameContainerState", gameContainerFacade.GetState(gameId));
    }

    public Task GetGameContext(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        return Clients.Caller.SendAsync("GameContext", gameContainerFacade.GetGameContext(gameId));
    }

    // [WithConnectionId]
    public async Task ConnectCommand(GameContainerConnectCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        command.ConnectionId = Context.ConnectionId;
        await gameContainerFacade.SendCommandAsync(command.GameId, command);
    }


    // [WithConnectionId]
    public async Task DisconnectCommand(GameContainerDisconnectCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        command.ConnectionId = Context.ConnectionId;
        await gameContainerFacade.SendCommandAsync(command.GameId, command);
    }
}
