using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.States;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public interface IKrokoGameContext : IGameContext
{
    KrokoGameState State { get; set; }

    KrokoLayoutContext Layout { get; set; }

    IDictionary<string, KrokoPlayerContext> PlayersDict { get; set; }

    IList<KrokoPlayerContext> Players { get; }

    IList<KrokoRound> Rounds { get; set; }

    KrokoRound CurrentRound { get; set; }

    IList<KrokoMessage> Messages { get; set; }

    DateTime? Timer { get; set; }

    KrokoPlayerContext? GetPlayerContextByUserId(string userId);

    KrokoPlayerContext GetPlayerContextByUserIdWithCheck(string userId);

    KrokoPlayerContext GetNextPlayer(string userId);

    void LoadRound(KrokoRound round);

    void ClearTimer();
}
