using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Database.DbContextInitializer;

public class DbContextInitializerFacade
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var initializers = scope.ServiceProvider.GetServices<IDbContextInitializer>();

        foreach (var initializer in initializers)
        {
            await initializer.InitializeAsync();
        }
    }
}
