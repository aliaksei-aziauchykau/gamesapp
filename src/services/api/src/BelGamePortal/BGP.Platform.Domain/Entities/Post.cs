using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Post : Entity<Guid>
{
    [Key]
    public Guid PostId { get; set; }

    public required string Title { get; init; }

    public required string Content { get; init; }

    public required Guid ContainerId { get; set; }

    public required Container Container { get; init; }

    public string? ViewComponent { get; init; }
}
