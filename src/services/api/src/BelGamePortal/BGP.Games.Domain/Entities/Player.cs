using System;
using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities;

[DataSource(DataSourceType.DbContext)]
public class Player : Entity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid? GameId { get; set; }

    public Game? Game { get; set; }
}
