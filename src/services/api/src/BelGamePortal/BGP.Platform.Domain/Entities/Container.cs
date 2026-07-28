using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Container : Entity<Guid>
{
    [Key]
    public Guid ContainerId { get; set; }

    public string? Name { get; set; }

    public ICollection<ProjectContainer> ProjectContainers { get; set; } = [];

    public ICollection<Post> Posts { get; set; } = [];

    public ICollection<Event> Events { get; set; } = [];

    public ICollection<ParentContainerChildContainer> ParentContainers { get; set; } = [];

    public ICollection<ParentContainerChildContainer> ChildContainers { get; set; } = [];
}
