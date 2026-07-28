using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using BGP.Shared.Modules.GameLogic.Kroko.Commands.JoinPlayer;
using BGP.Shared.Modules.GameLogic.Kroko.Commands.LeavePlayer;
using BGP.Shared.Modules.GameLogic.Kroko.GameCommands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Hubs;

public class KrokoGameHub : Hub
{
    public Task GetGameContextCommand(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        var gameContext = gameContainerFacade.GetGameContext(gameId);
        return Clients.Caller.SendAsync("GameContext", gameContext);
    }

    public Task GetGameSettingsCommand(string gameId, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        var gameSettings = gameContainerFacade.GetGameSettings(gameId);
        return Clients.Caller.SendAsync("GameSettings", gameSettings);
    }

    // [WithConnectionId]
    public async Task JoinPlayerCommand(KrokoJoinPlayerGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        command.ConnectionId = Context.ConnectionId;
        await gameContainerFacade.SendCommandAsync(command.GameId, command);
    }

    // [WithConnectionId]
    public async Task LeavePlayerCommand(KrokoLeavePlayerGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        command.ConnectionId = Context.ConnectionId;
        await gameContainerFacade.SendCommandAsync(command.GameId, command);
    }

    // [WithConnectionId]
    public async Task ChangePlayerContextCommand(KrokoChangePlayerContextCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        command.ConnectionId = Context.ConnectionId;
        await gameContainerFacade.SendCommandAsync(command.GameId, command);
    }

    public void StartGameCommand(KrokoStartGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.SendGameCommand(command.GameId, command);
    }

    public void GuessWordCommand(KrokoGuessWordGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.SendGameCommand(command.GameId, command);
    }

    public void DrawWordCommand(KrokoDrawWordGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.SendGameCommand(command.GameId, command);
    }

    public void StoreSelectedWordCommand(KrokoStoreSelectedWordGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.SendGameCommand(command.GameId, command);
    }

    public void SaveGameSettingsCommand(KrokoStoreSelectedWordGameCommand command, [FromServices] IGameContainerFacade gameContainerFacade)
    {
        gameContainerFacade.SendGameCommand(command.GameId, command);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

}
