using BGP.Shared.Libs.GameLogic;

namespace BGP.Shared.Modules.GameLogic.Kroko;

public class KrokoGameSettings : GameSettings, IKrokoGameSettings
{
    public KrokoGameSettings() : base()
    {
    }

    public KrokoGameSettings(string gameId) : base(gameId)
    {
    }

    public int NumberOfRounds { get; set; } = 20;
}
