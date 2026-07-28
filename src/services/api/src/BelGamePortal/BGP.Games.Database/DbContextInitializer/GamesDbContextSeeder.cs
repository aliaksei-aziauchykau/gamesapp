using BGP.Shared.Libs.Database.DbContextInitializer;

namespace BGP.Games.Database;

public class GamesDbContextSeeder : IDbContextSeeder
{
    private readonly GamesDbContext _context;

    public GamesDbContextSeeder(GamesDbContext context)
    {
        _context = context;
    }


    public async Task SeedAsync()
    {
        // if (_context.Emails.Any()) return;

        // await _context.Emails.AddRangeAsync(
        // [
        //     new Email
        //     {
        //         Subject = "Welcome to the Game Portal",
        //         Body = "Welcome to the Game Portal. We are excited to have you here.",
        //         From = "aletx",
        //         To = "ref"

        //     },
        //     new Email
        //     {
        //         Subject = "Game Portal - Password Reset",
        //         Body = "You have requested a password reset. Please click the link below to reset your password.",
        //         From = "refe",
        //         To = "refe"
        //     }
        // ]);
        // await _context.SaveChangesAsync();
    }
}

