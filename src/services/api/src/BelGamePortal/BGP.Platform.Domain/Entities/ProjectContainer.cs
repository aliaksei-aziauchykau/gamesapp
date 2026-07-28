using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class ProjectContainer : Entity<int>
{
    [Key]
    public int ProjectContainerId { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    public Guid ContainerId { get; set; }
    public Container Container { get; set; }
}
