namespace BGP.Shared.Libs.GameLogic;

public interface IGameLogic
{
    public event EventHandler<string> GameStateWasChanged;

    string CurrentGameStateName { get; }

    Task HandleGameCommandAsync(GameCommand command);

    Task ChangeGameStateToAsync<TGameState>() where TGameState : IGameState;

    IGameState GetGameState<TGameState>() where TGameState : IGameState;
}
