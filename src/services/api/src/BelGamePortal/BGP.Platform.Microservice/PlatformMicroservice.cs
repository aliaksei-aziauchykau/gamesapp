using BGP.Platform.Database;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Libs.Microservice;

namespace BGP.Platform.Microservice;

public class PlatformMicroservice(IServiceProvider serviceProvider) : BaseMicroservice(serviceProvider), IPlatformMicroservice
{
    public override async Task RunAsync()
    {
        await PlatformDatabaseModule.MigrateAndSeedAsync(serviceProvider);
    }
}
