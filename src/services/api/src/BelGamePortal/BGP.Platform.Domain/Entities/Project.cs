using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Project : Entity<Guid>
{
    [Key]
    public Guid ProjectId { get; set; }

    public string Name { get; set; }

    public Guid WorkspaceId { get; set; }

    public Workspace Workspace { get; set; }

    public int? ProjectVisibilityId { get; set; }

    public ProjectVisibility ProjectVisibility { get; set; }

    public ICollection<ProjectContainer> ProjectContainers { get; set; } = [];
}
