namespace BGP.Shared.Modules.GameLogic.Kroko.PlayerContext;

public class KrokoPlayerGameContext : IKrokoPlayerGameContext
{
    public IList<string> SuggestedWords { get; set; } = new List<string>();
}
