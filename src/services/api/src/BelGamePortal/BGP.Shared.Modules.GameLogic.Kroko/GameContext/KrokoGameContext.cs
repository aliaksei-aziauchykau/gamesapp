using BGP.Shared.Modules.GameLogic.Kroko.States;
using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.GameLogic;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoGameContext : GameContext, IKrokoGameContext
{
    public KrokoGameContext() : base()
    {
    }

    public KrokoGameContext(string gameId) : base(gameId)
    {
    }

    public KrokoGameState State { get; set; } = KrokoGameState.Intitial;

    public string Name { get; set; }

    public KrokoLayoutContext Layout { get; set; } = new KrokoLayoutContext();

    public IDictionary<string, KrokoPlayerContext> PlayersDict { get; set; } = new Dictionary<string, KrokoPlayerContext>();

    public IList<KrokoPlayerContext> Players => this.PlayersDict.Values.ToList();

    public IList<KrokoRound> Rounds { get; set; } = new List<KrokoRound>();

    public IList<KrokoMessage> Messages { get; set; } = new List<KrokoMessage>();

    public KrokoRound CurrentRound { get; set; } = new KrokoRound();

    public DateTime? Timer { get; set; }

    public KrokoPlayerContext GetNextPlayer(string userId)
    {
        if (PlayersDict.Count == 0) throw new InvalidOperationException();

        if (PlayersDict.Count == 1) return Players[0];

        var linkedList = new LinkedList<KrokoPlayerContext>(this.Players);

        var currentPlayer = linkedList.FirstOrDefault(x => x.UserId == userId)
            ?? throw new NullReferenceException();

        var listPlayer = linkedList.Find(currentPlayer)
            ?? throw new NullReferenceException();

        var nextPlayer = listPlayer.Next ?? linkedList.First;

        return nextPlayer.Value;
    }

    public KrokoPlayerContext? GetPlayerContextByUserId(string userId)
    {
        this.PlayersDict.TryGetValue(userId, out var player);

        return player;
    }

    public KrokoPlayerContext GetPlayerContextByUserIdWithCheck(string userId)
    {
        var player = this.GetPlayerContextByUserId(userId);

        return player ?? throw new NullReferenceException("PlayerContext is not found");
    }

    public void LoadRound(KrokoRound round)
    {
        this.Rounds.Add(round);
        this.CurrentRound = round;
    }

    public void ClearTimer()
    {
        this.Timer = null;
    }
}
