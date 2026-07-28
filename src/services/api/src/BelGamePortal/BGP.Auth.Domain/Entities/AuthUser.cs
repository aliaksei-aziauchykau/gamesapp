using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Auth.Domain.Entities;

public class AuthUser : Entity<Guid>
{
    [Key]
    public Guid Id { get; set; }
}
