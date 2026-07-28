using System.Collections.Concurrent;
using MediatR;
using Serilog;

namespace BGP.Shared.Libs.GameLogic;

public class GamePerformer : IGamePerformer
{
    private bool isRunningLoop = true;
    private readonly object lockObject = new object();
    private readonly AutoResetEvent inputEvent = new AutoResetEvent(false);
    private readonly ConcurrentQueue<GameCommand> _commandQueue = new ConcurrentQueue<GameCommand>();
    public Thread gameThread = null;
    private bool _isRunning = false;

    private IGameLogic _gameLogic;

    public bool IsRunning => _isRunning;

    public ConcurrentQueue<GameCommand> CommandQueueu => _commandQueue;

    public GamePerformer(IGameLogic gameLogic)
    {
        _gameLogic = gameLogic;
    }

    public void Start()
    {
        Log.Information("GamePerformerStarted");
        gameThread = new Thread(GameLoop);
        gameThread.Start();
        _isRunning = true;
    }

    public void SendCommand(GameCommand command)
    {
        // Simulate input received externally
        lock (lockObject)
        {
            // Enqueue the input message
            _commandQueue.Enqueue(command);

            // Signal the input event
            inputEvent.Set();
        }
    }

    private async void GameLoop()
    {
        while (isRunningLoop)
        {
            // Wait for input
            inputEvent.WaitOne();

            // Process the input messages
            while (_commandQueue.TryDequeue(out var command))
            {
                Log.Information("Get Command: {Command}", command);

                await _gameLogic.HandleGameCommandAsync(command);

                if (command is StopGameCommand)
                {
                    isRunningLoop = false;
                    break;
                }
            }
        }
    }

    public void Stop()
    {
        Log.Information("GamePerformerStop");
        this.SendCommand(new StopGameCommand());
        _isRunning = false;
    }
}
