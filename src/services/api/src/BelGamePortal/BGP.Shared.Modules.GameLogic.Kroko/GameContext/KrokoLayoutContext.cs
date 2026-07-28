using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoLayoutContext
{
    public bool IsBoardDisplayed { get; set; }

    public void ShowBoard()
    {
        IsBoardDisplayed = true;
    }
}
