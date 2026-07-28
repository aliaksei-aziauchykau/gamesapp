using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Event : Entity<Guid>
{
    [Key]
    public Guid EventId { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Location { get; init; }

    public required DateTime StartDate { get; init; }

    public required DateTime EndDate { get; init; }

    public required Guid ContainerId { get; init; }

    public required Container Container { get; init; }

    public string? ViewComponent { get; init; }
}
