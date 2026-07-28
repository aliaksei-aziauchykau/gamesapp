using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class ProjectVisibility : Entity<int>
{
    [Key]
    public int ProjectVisibilityId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<Project> Projects { get; set; } = [];
}
