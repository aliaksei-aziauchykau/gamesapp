

using BGP.Games.Database;
using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Database.DbContextInitializer;

public class GameDefinitionDbContextSeeder : IDbContextSeeder
{
    private readonly GamesDbContext _context;

    public GameDefinitionDbContextSeeder(GamesDbContext context)
    {
        _context = context;
    }


    public async Task SeedAsync()
    {

        foreach (var gameDefinition in GetGameDefinition())
        {
            if (!_context.GameDefinitions.Any(x => x.Name == gameDefinition.Name))
            {
                await _context.GameDefinitions.AddAsync(gameDefinition);

                continue;
            }

            if (_context.GameDefinitions.Any(x => x.Name == gameDefinition.Name))
            {
                var gameDefinitionToUpdate = _context.GameDefinitions.FirstOrDefault(x => x.Name == gameDefinition.Name);
                if (gameDefinitionToUpdate == null) continue;

                gameDefinitionToUpdate.Description = gameDefinition.Description;
                gameDefinitionToUpdate.ImageUrl = gameDefinition.ImageUrl;
                gameDefinitionToUpdate.ImageAlt = gameDefinition.ImageAlt;
                gameDefinitionToUpdate.GamePageAppUrl = gameDefinition.GamePageAppUrl;

                _context.GameDefinitions.Update(gameDefinitionToUpdate);
            }
        }

        await _context.SaveChangesAsync();
    }

    private GameDefinition[] GetGameDefinition()
    {
        return new GameDefinition[]
        {
            new GameDefinition
            {
                Name = "Kroko",
                Description = "Draw and go go",
                ImageUrl = "assets/images/brushes-paint-and-palletes.png",
                GamePageAppUrl = "kroko",
            },
            new GameDefinition {
                Name = "GeoGuesser",
                Description = "Find a belarussian place",
                ImageUrl = "assets/images/piece-of-papper-map.png",
                GamePageAppUrl = "geo-guesser"
            },
             new GameDefinition {
                Name = "PhaserTest",
                Description = "PhaserTestGame",
                ImageUrl = "assets/images/piece-of-papper-map.png",
                GamePageAppUrl = "4geo-guesser"
            }
        };
    }
}
