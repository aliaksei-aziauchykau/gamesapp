using System.Collections.Concurrent;

namespace BGP.Shared.Libs.GameLogic;

public interface IGamePerformer
{
    bool IsRunning { get; }

    ConcurrentQueue<GameCommand> CommandQueueu { get; }

    void Start();

    void Stop();

    void SendCommand(GameCommand command);
}
