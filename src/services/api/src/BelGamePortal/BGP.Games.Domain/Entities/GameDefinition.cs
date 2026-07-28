using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class GameDefinition : Entity<int>
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageAlt { get; set; }

    public string? GamePageAppUrl { get; set; }

    public ICollection<Game> Games { get; set; } = new List<Game>();
}
