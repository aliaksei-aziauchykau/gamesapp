namespace BGP.Shared.Libs.GameLogic;

public interface IGameRestorer
{
    Task RestoreAsync(IGameContext gameContext);
}
