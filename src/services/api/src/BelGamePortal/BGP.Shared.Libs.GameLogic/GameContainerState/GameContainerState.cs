using System.Collections.Concurrent;

namespace BGP.Shared.Libs.GameLogic;

public class GameContainerState : IGameContainerState
{
    public bool isRunning { get; set; }

    public bool Created { get; set; }

    public bool Initialized { get; set; }

    public string? CurrentGameStateName { get; set; }

    public ConcurrentQueue<GameCommand> CommandsQueue { get; set; } = new ConcurrentQueue<GameCommand>();
}
