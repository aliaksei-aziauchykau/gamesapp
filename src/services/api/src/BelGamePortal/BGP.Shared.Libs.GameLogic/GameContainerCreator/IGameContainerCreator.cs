namespace BGP.Shared.Libs.GameLogic;

public interface IGameContainerCreator
{
    string Name { get; }

    IGameContainer Create(string gameId);
}
