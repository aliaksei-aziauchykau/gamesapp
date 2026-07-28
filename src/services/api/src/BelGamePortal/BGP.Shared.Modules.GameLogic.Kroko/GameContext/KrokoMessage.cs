using System.Text.Json.Serialization;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoMessage
{
    [JsonIgnore]
    public string UserId { get; set; }

    public string PlayerName { get; set; }

    public string Message { get; set; }
}
