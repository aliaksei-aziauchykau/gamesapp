namespace BGP.Shared.Libs.Database.DbContextInitializer;

public interface IDbContextSeeder
{
    public int Priority => 0;

    Task SeedAsync();
}
