using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Game : Entity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string Code { get; set; }

    public GameDefinition? GameDefinition { get; set; }

    public int? GameDefinitionId { get; set; }

    public ICollection<Player> Players { get; set; } = new List<Player>();
}
