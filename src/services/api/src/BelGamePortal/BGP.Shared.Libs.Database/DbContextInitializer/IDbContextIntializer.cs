using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Database.DbContextInitializer;

public interface IDbContextInitializer
{
    Task InitializeAsync();
}

