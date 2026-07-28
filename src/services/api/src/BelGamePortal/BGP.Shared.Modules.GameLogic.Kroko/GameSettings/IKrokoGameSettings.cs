using BGP.Shared.Libs.GameLogic;

namespace BGP.Shared.Modules.GameLogic.Kroko;

public interface IKrokoGameSettings : IGameSettings
{
    int NumberOfRounds { get; set; }
}
