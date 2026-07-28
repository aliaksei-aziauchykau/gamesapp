using Serilog;
using Unity;

namespace BGP.Shared.Libs.GameLogic;

public class GameLogic : IGameLogic
{
    private IGameState? currentGameState;

    public event EventHandler<string> GameStateWasChanged;

    protected readonly IUnityContainer _container;

    public string CurrentGameStateName => this.currentGameState?.GetType().Name ?? string.Empty;

    public GameLogic(IUnityContainer container)
    {
        this._container = container;
    }

    public virtual async Task HandleGameCommandAsync(GameCommand command)
    {
        if (command is ChangeGameStateCommand changeGameStateCommand)
        {
            await HandleChangeStateCommandAsync(changeGameStateCommand);
            return;
        }

        if (currentGameState == null) throw new InvalidOperationException("Game state is not set");

        if (!currentGameState.isCommandAccepted(command)) return;

        await currentGameState.HandleGameCommandAsync(command);
    }

    private void SetState(IGameState state)
    {
        currentGameState = state;
    }

    private async Task HandleChangeStateCommandAsync(ChangeGameStateCommand command)
    {
        Log.Information("Game State was changed {0}", command.GameState.GetType().Name);
        GameStateWasChanged?.Invoke(this, command.GameState.GetType().Name);

        if (currentGameState != null) await currentGameState.OnLeaveAsync();
        SetState(command.GameState);
        if (currentGameState != null) await currentGameState.OnEntryAsync();
        if (command.GameCommand != null) await this.HandleGameCommandAsync(command.GameCommand);
    }

    public IGameState GetGameState<TGameState>() where TGameState : IGameState
    {
        return _container.Resolve<TGameState>();
    }

    public async Task ChangeGameStateToAsync<TGameState>() where TGameState : IGameState
    {
        var nextGameState = GetGameState<TGameState>();
        await HandleGameCommandAsync(new ChangeGameStateCommand { GameState = nextGameState });
    }
}
