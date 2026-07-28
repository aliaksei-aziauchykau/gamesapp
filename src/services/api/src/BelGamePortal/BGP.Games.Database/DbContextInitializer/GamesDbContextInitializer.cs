using BGP.Shared.Libs.Database.DbContextInitializer;
using Microsoft.EntityFrameworkCore;

namespace BGP.Games.Database;

public class GamesDbContextInitializer : IDbContextInitializer
{
    private readonly GamesDbContext context;

    private readonly IEnumerable<IDbContextSeeder> dbContextSeeders;

    public GamesDbContextInitializer(GamesDbContext context, IEnumerable<IDbContextSeeder> dbContextSeeders)
    {
        this.context = context;
        this.dbContextSeeders = dbContextSeeders;
    }

    public async Task InitializeAsync()
    {
        await MigrateAsync();

        var seeders = dbContextSeeders.OrderByDescending(x => x.Priority);

        foreach (var dbContextInitializer in seeders)
        {
            await dbContextInitializer.SeedAsync();
        }
    }

    private async Task MigrateAsync()
    {
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            // Log.Error(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}
