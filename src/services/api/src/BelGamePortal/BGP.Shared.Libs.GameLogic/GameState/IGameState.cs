namespace BGP.Shared.Libs.GameLogic;

public interface IGameState
{
    bool isCommandAccepted(GameCommand command);

    Task OnEntryAsync();

    Task HandleGameCommandAsync(GameCommand command);

    Task OnLeaveAsync();
}
