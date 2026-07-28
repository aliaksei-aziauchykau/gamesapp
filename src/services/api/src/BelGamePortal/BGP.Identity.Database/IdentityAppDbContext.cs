using BGP.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BGP.Identity.Database;

public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<User, UserRole, Guid>(options)
{
    public const string Schema = "identity";

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Schema);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityAppDbContext).Assembly);
    }
}
