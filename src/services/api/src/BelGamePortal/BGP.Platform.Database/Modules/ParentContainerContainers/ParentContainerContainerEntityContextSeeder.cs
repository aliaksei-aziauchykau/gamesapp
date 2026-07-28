

using BGP.Platform.Database;
using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Database.DbContextInitializer;

public class ParentContainerContainersDbContextSeeder : IDbContextSeeder
{
    private readonly PlatformDbContext _context;

    public ParentContainerContainersDbContextSeeder(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
    }
}
