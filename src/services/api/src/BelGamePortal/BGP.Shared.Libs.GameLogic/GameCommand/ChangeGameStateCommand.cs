namespace BGP.Shared.Libs.GameLogic;

public class ChangeGameStateCommand : GameCommand
{
    public IGameState GameState { get; set; }

    public GameCommand? GameCommand { get; set; }
}
