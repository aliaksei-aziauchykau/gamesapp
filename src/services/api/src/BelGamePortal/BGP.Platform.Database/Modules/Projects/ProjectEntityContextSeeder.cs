

using BGP.Platform.Database;
using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Database.DbContextInitializer;

public class ProjectsDbContextSeeder : IDbContextSeeder
{
    private readonly PlatformDbContext _context;

    public ProjectsDbContextSeeder(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
    }
}
