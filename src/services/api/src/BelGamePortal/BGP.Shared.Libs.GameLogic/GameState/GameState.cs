
namespace BGP.Shared.Libs.GameLogic;

public abstract class GameState : IGameState
{
    protected virtual IList<Type> AcceptableCommandTypes => new List<Type>();

    public virtual Task HandleGameCommandAsync(GameCommand command)
    {
        return Task.CompletedTask;
    }

    public virtual bool isCommandAccepted(GameCommand command)
    {
        return AcceptableCommandTypes.Contains(command.GetType());
    }

    public virtual Task OnEntryAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnLeaveAsync()
    {
        return Task.CompletedTask;
    }
}
