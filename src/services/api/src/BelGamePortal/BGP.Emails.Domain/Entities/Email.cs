

using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Emails.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Email : Entity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string? Subject { get; init; } = default!;

    public string From { get; init; } = default!;

    public string To { get; init; } = default!;

    public string Body { get; init; } = default!;
}
