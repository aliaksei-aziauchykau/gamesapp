using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities.Games.Kroko;

[DataSource(DataSourceType.DbContext)]
public class KrokoWordLanguage : Entity<int>
{
    [Key]
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public ICollection<KrokoWord> Words { get; set; } = new List<KrokoWord>();
}
