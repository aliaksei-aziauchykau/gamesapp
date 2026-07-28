namespace BGP.Shared.Libs.GameLogic;

public interface IGameStateRestorer
{
    IGameState Restore(IGameContext context);
}
