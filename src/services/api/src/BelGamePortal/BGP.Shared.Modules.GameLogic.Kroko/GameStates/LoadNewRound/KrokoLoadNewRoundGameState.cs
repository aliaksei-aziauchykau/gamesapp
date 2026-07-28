using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using BGP.Shared.Modules.GameLogic.Kroko.States;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameStates;

public class KrokoLoadNewRoundGameState : GameState, IKrokoLoadNewRoundGuessedGameState
{
    private readonly IMediator _mediator;
    private readonly IGameLogic _gameLogic;
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;
    private readonly IKrokoPlayerSelectWordGameState _krokoPlayerSelectWordGameState;

    public KrokoLoadNewRoundGameState(IMediator mediator, IGameLogic gameLogic, IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext, IKrokoPlayerSelectWordGameState krokoPlayerSelectWordGameState)
    {
        _mediator = mediator;
        _gameLogic = gameLogic;
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
        _krokoPlayerSelectWordGameState = krokoPlayerSelectWordGameState;
    }

    //protected override IList<Type> AcceptableCommandTypes => new List<Type> { 
    //    typeof(KrokoStoreSelectedWordGameCommand)
    //};

    public override async Task OnEntryAsync()
    {
        var gameId = _gameContext.GameId;

        if (_gameContext.Rounds.Count == 3)
        {
            await _gameLogic.ChangeGameStateToAsync<KrokoGameCompletedGameState>();

            return;
        }

        var currentUserId = _gameContext.CurrentRound.DrawerUserId;

        var nextPlayer = _gameContext.GetNextPlayer(currentUserId);

        var newRound = new KrokoRound();

        newRound.SetDrawerUserId(nextPlayer.UserId);

        _gameContext.LoadRound(newRound);

        //var currentUserId = this._gameContext.CurrentRound.DrawerUserId;
        //await SelectWord(currentUserId, currentUserId);

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);

        await _krokoGameHubContext.Clients.Groups(new[] { _gameContext.GameId }).SendAsync("ReloadShapesCommand");

        await _gameLogic.HandleGameCommandAsync(new ChangeGameStateCommand { GameState = _krokoPlayerSelectWordGameState });
    }


    //public override async Task HandleGameCommandAsync(GameCommand command)
    //{
    //    await _mediator.Send(command);

    //    await _gameLogic.HandleGameCommandAsync( new ChangeGameStateCommand { GameState = _krokoDrawGuessWordGameState });
    //}
}
