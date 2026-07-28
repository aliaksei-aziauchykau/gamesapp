using System.Collections.Concurrent;

namespace BGP.Shared.Libs.GameLogic;

public interface IGameContainerState
{
    bool isRunning { get; set; }

    bool Created { get; set; }

    bool Initialized { get; set; }

    public string? CurrentGameStateName { get; set; }

    ConcurrentQueue<GameCommand> CommandsQueue { get; set; }
}
