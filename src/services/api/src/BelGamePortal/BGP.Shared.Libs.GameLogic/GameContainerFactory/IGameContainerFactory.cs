using System.Diagnostics.CodeAnalysis;

namespace BGP.Shared.Libs.GameLogic;

public interface IGameContainerFactory
{
    void Create(string gameDef, string gameId);

    bool TryGetValue(string gameId, [MaybeNullWhen(false)] out IGameContainer gameContainer);

    void Destroy(string gameId);

    string GetListOfContainers();
}
