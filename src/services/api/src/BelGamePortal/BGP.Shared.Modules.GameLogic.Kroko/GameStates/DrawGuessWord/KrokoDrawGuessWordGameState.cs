using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Attributes;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using Hangfire;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoDrawGuessWordGameState : GameState, IKrokoDrawGuessWordGameState
{
    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;
    private readonly IKrokoWordWasGuessedGameState _krokoWordWasGuessedGameState;
    private readonly IKrokoWordWasNotGuessedGameState _krokoWordWasNotGuessedGameState;

    private string timeIsOutJobId;

    public KrokoDrawGuessWordGameState(
        IMediator mediator,
        IKrokoGameContext gameContext,
        IGameLogic gameLogic,
        IKrokoWordWasGuessedGameState krokoWordWasGuessedGameState,
        IKrokoWordWasNotGuessedGameState krokoWordWasNotGuessedGameState,
        IKrokoGameHubContext krokoGameHubContext)
    {
        _mediator = mediator;
        _gameContext = gameContext;
        _gameLogic = gameLogic;
        _krokoWordWasGuessedGameState = krokoWordWasGuessedGameState;
        _krokoWordWasNotGuessedGameState = krokoWordWasNotGuessedGameState;
        _krokoGameHubContext = krokoGameHubContext;
    }

    protected override IList<Type> AcceptableCommandTypes => new List<Type> {
        typeof(KrokoGuessWordGameCommand),
        typeof(KrokoDrawWordGameCommand)
    };

    public override async Task OnEntryAsync()
    {
        var timeSpan = TimeSpan.FromMinutes(3);
        var date = DateTime.UtcNow.Add(timeSpan);

        _gameContext.Timer = date;

        timeIsOutJobId = BackgroundJob.Schedule(
             () => this.TimeIsRunOutNoOneGuessJob(_gameContext.GameId),
             TimeSpan.FromMinutes(3));

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);
    }

    public override async Task OnLeaveAsync()
    {
        _gameContext.ClearTimer();

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);

        BackgroundJob.Delete(this.timeIsOutJobId);
    }

    public override async Task HandleGameCommandAsync(GameCommand command)
    {
        await _mediator.Send(command);

        if (command is KrokoGuessWordGameCommand && _gameContext.CurrentRound.IsWordWasGuessed)
        {
            await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = _krokoWordWasGuessedGameState });
        }
    }

    [GameContainerJob]
    public async Task TimeIsRunOutNoOneGuessJob(string _)
    {
        await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = _krokoWordWasNotGuessedGameState });
    }
}
