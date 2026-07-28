using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Database.DbContextInitializer;
using Microsoft.EntityFrameworkCore.Storage;

namespace BGP.Platform.Database;

public class PlatformDbContextSeeder : IDbContextSeeder
{
    private readonly PlatformDbContext _context;

    public int Priority => 0;

    public PlatformDbContextSeeder(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Workspaces.Any()) return;

        var workspace = new Workspace
        {
            Name = "Default Workspace",
            Projects = [
                new Project
                {
                    Name = "Default Project",
                    ProjectVisibilityId = 1,
                }
            ]
        };

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();
    }
}

