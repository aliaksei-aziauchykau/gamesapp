

using BGP.Platform.Database;
using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Database.DbContextInitializer;

public class ProjectVisibilitiesDbContextSeeder : IDbContextSeeder
{
    private readonly PlatformDbContext _context;

    public int Priority => 1;

    public ProjectVisibilitiesDbContextSeeder(PlatformDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (this._context.ProjectVisibilities.Any()) return;

        var projectVisibilities = new List<ProjectVisibility> {
            new ProjectVisibility {
                Name = "Public",
                Description = "Public visibility"
            },
            new ProjectVisibility {
                Name = "Private",
                Description = "Private visibility"
            }
        };

        await this._context.ProjectVisibilities.AddRangeAsync(projectVisibilities);
        await this._context.SaveChangesAsync();
    }
}
