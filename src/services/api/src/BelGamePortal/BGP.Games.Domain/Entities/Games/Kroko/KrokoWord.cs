using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities.Games.Kroko;

[DataSource(DataSourceType.DbContext)]
public class KrokoWord : Entity<int>
{
    [Key]
    public int Id { get; set; }

    public string Word { get; set; }

    public string Meaning { get; set; } = string.Empty;

    [ForeignKey("KrokoWordLanguage")]
    public int LanguageId { get; set; }

    public KrokoWordLanguage Language { get; set; }
}
