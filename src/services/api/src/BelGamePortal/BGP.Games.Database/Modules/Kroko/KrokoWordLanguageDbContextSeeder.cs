using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Shared.Libs.Database.DbContextInitializer;

namespace BGP.Games.Database.Modules.Kroko;

public class KrokoWordLanguageDbContextSeeder : IDbContextSeeder
{
    private readonly GamesDbContext _context;

    public int Priority => 1;

    public KrokoWordLanguageDbContextSeeder(GamesDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {

        if (_context.KrokoWordLanguages.Any()) return;

        await _context.KrokoWordLanguages.AddRangeAsync(
            new KrokoWordLanguage[]
            {
                new KrokoWordLanguage { Code = "By", Name = "Belarussian" },
                new KrokoWordLanguage { Code = "En", Name = "English" },
                new KrokoWordLanguage { Code = "Ru", Name = "Russian" },
            });
        await _context.SaveChangesAsync();
    }
}
