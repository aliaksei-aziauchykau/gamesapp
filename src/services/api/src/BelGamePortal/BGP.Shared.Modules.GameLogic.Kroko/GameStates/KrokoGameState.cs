namespace BGP.Shared.Modules.GameLogic.Kroko.States;

public enum KrokoGameState
{
    Intitial = 0,
    Preparation = 1,
    StartRound = 2,
    ChooseMasterPlayer = 3,
    PreDrawing = 4,
    Drawing = 5,
    PlayerGuessWord = 6,
    NoOneGuessedWord = 7,
    ShowWordAndWinner = 8,
    ShowWord = 9,
    EndOrProceedGame = 10,
    ShowFinalResult = 11,
}
