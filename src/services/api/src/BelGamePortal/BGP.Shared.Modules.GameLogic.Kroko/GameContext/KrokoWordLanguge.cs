


using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoWordLanguage : IHasKey<int>
{
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public ICollection<KrokoWord> Words { get; set; } = new List<KrokoWord>();
}
