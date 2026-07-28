using System.Text.Json.Serialization;
using BGP.Shared.Modules.GameLogic.Kroko.PlayerContext;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoPlayerContext
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }

    public string Nickname { get; set; }

    public IList<string> ConnectionIds { get; set; } = new List<string>();

    public int Score { get; set; }

    public bool IsMaster { get; set; }

    [JsonIgnore]
    public IKrokoPlayerGameContext GameContext { get; set; } = new KrokoPlayerGameContext();
}
