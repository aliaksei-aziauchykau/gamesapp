using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Attributes;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using Hangfire;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoChangeDrawerGameState : GameState, IKrokoChangeDrawerGameState
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;
    private readonly IGameLogic _gameLogic;

    public KrokoChangeDrawerGameState(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext, IGameLogic gameLogic)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
        _gameLogic = gameLogic;
    }

    public override Task OnEntryAsync()
    {
        var gameId = _gameContext.GameId;

        BackgroundJob.Schedule(
            () => this.SetNextPlayerJob(gameId)
            ,
            TimeSpan.FromSeconds(20));

        return Task.CompletedTask;
    }

    [GameContainerJob]
    public async Task SetNextPlayerJob(string _)
    {
        await SetNextPlayer();
    }

    private async Task SetNextPlayer()
    {
        if (_gameContext.PlayersDict.Count <= 1) return;

        var nextPlayer = _gameContext.GetNextPlayer(_gameContext.CurrentRound.DrawerUserId);

        _gameContext.CurrentRound.SetDrawerUserId(nextPlayer.UserId);

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);

    }

}
