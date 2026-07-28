using BGP.Shared.Libs.CrudServices.Interfaces.Services;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Attributes;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using BGP.Shared.Modules.GameLogic.Kroko.Interfaces;
using Hangfire;
using MediatR;
using Serilog;
using System.Linq;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoPlayerSelectWordGameState : GameState, IKrokoPlayerSelectWordGameState
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;
    private readonly IKrokoStoreSelectedWordGameState _krokoStoreSelectedWordGame;
    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;

    private readonly IKrokoGameDataService _krokoGameDataService;

    public KrokoPlayerSelectWordGameState(
        IKrokoGameContext gameContext,
        IKrokoGameHubContext krokoGameHubContext,
        IMediator mediator,
        IGameLogic gameLogic,
        IKrokoStoreSelectedWordGameState krokoStoreSelectedWordGame,
        IKrokoGameDataService krokoGameDataService)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
        _mediator = mediator;
        _gameLogic = gameLogic;
        _krokoStoreSelectedWordGame = krokoStoreSelectedWordGame;
        _krokoGameDataService = krokoGameDataService;
    }

    protected override IList<Type> AcceptableCommandTypes => new List<Type> {
        typeof(KrokoStoreSelectedWordGameCommand)
    };

    public override async Task OnEntryAsync()
    {
        var gameId = _gameContext.GameId;

        if (_gameContext.PlayersDict.Count == 0 || _gameContext.CurrentRound.WordToGuess != null) return;

        var currentUserId = this._gameContext.CurrentRound.DrawerUserId;

        await SelectWord(gameId, currentUserId, currentUserId);

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);

        RecurringJob.AddOrUpdate(
            $"{gameId}-SelectWordJob",
            () => this.SelectWordJob(gameId),
            "*/30 * * * * *");
    }

    public override Task OnLeaveAsync()
    {
        Log.Information("Leave O");

        RecurringJob.RemoveIfExists($"{_gameContext.GameId}-SelectWordJob");

        return Task.CompletedTask;
    }

    public override async Task HandleGameCommandAsync(GameCommand command)
    {
        await _mediator.Send(command);

        await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = _krokoStoreSelectedWordGame, GameCommand = command });
    }

    [GameContainerJob]
    public async Task SelectWordJob(string _)
    {
        if (_gameContext.PlayersDict.Count == 0) return;

        var currentUserId = _gameContext.CurrentRound.DrawerUserId;

        var nextPlayerUserId = _gameContext.GetNextPlayer(_gameContext.CurrentRound.DrawerUserId).UserId;

        _gameContext.CurrentRound.SetDrawerUserId(nextPlayerUserId);

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);

        await SelectWord(_gameContext.GameId, currentUserId, nextPlayerUserId);
    }

    private async Task SelectWord(string gameId, string previousPlayerUserId, string nextPlayerUserId)
    {
        await _krokoGameHubContext.SendToPlayerAsync(gameId, previousPlayerUserId, "CloseAllModalsCommand");

        var queryable = await _krokoGameDataService.GetRandomWordsAsync();

        var words = queryable.OrderBy(x => Guid.NewGuid()).Take(3).ToList();

        await _krokoGameHubContext.SendToPlayerAsync(gameId, nextPlayerUserId, "SelectWordCommand", words);
    }
}
