using BGP.Shared.Libs.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BGP.Identity.Domain.Entities;

public class User : IdentityUser<Guid>, IHasKey<Guid>, IAuditable
{
    public DateTime Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? Updated { get; set; }
    public string? UpdatedBy { get; set; }
}
