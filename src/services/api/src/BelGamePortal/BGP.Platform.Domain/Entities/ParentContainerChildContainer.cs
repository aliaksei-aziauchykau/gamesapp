using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class ParentContainerChildContainer : Entity<int>
{
    [Key]
    public int ParentContainerChildContainerId { get; set; }

    public Guid ParentContainerId { get; set; }
    public Container ParentContainer { get; set; }

    public Guid ChildContainerId { get; set; }
    public Container ChildContainer { get; set; }
}
