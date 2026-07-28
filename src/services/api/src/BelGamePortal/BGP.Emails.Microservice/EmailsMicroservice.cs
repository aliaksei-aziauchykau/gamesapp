using BGP.Emails.Database;
using BGP.Emails.Microservice.Interfaces;
using BGP.Shared.Libs.Microservice;

namespace BGP.Emails.Microservice;
public class EmailsMicroservice(IServiceProvider serviceProvider) : BaseMicroservice(serviceProvider), IEmailsMicroservice
{
    public override async Task RunAsync()
    {
        await EmailsDatabaseModule.MigrateAndSeedAsync(serviceProvider);
    }
}
