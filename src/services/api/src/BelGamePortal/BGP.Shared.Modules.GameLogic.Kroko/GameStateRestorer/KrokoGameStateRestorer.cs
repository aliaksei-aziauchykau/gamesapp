using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.States;

namespace BGP.Shared.Modules.GameLogic.Kroko.StateRestorer;

public class KrokoGameStateRestorer : IGameStateRestorer
{
    private readonly IKrokoInitialGameState _initialGameState;

    public KrokoGameStateRestorer(IKrokoInitialGameState initialGameState)
    {
        _initialGameState = initialGameState;
    }

    public IGameState Restore(IGameContext context)
    {
        return _initialGameState;
    }
}
