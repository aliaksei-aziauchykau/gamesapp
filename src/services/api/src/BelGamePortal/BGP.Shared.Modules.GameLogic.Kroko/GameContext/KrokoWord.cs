
using System.ComponentModel.DataAnnotations.Schema;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Modules.GameLogic.Kroko.Context;

public class KrokoWord : IHasKey<int>
{
    public int Id { get; set; }

    public string Word { get; set; }

    public string Meaning { get; set; } = string.Empty;

    [ForeignKey("KrokoWordLanguage")]
    public int LanguageId { get; set; }

    public KrokoWordLanguage Language { get; set; }
}
