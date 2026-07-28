using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Platform.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Workspace : Entity<Guid>
{
    [Key]
    public Guid WorkspaceId { get; set; }

    public string Name { get; set; }

    public string? OwnerUserId { get; set; }

    public ICollection<Project> Projects { get; set; }
}
