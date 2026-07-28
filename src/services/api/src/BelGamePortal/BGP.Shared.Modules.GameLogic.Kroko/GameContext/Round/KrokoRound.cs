using System.Text.Json.Serialization;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoRound
{
    public string ShapesJson { get; set; }

    public string Index { get; set; }

    public string DrawerUserId { get; private set; }

    [JsonIgnore]
    public string? WordToGuess { get; set; }

    public KrokoPlayerContext? Winner { get; set; }

    public string WinnerUserId { get; set; }

    public void SetDrawerUserId(string userId)
    {
        this.DrawerUserId = userId;
    }

    public bool IsWordWasGuessed => this.Winner is not null;
}
