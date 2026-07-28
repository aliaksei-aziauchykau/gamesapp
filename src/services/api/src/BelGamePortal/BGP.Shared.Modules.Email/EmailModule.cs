
using Microsoft.Extensions.DependencyInjection;
namespace BGP.Shared.Modules.Email;

public class EmailModule
{
    private IServiceCollection? _services;

    private IServiceCollection Services => _services ?? throw new ArgumentNullException(nameof(_services));

    public static EmailModule Create()
    {
        return new EmailModule();
    }

    public EmailModule AddServiceCollection(IServiceCollection services)
    {
        this._services = services;

        return this;

    }

    public EmailModule AddServices(IEmailConfiguration emailConfiguration)
    {

        Services.AddSingleton(emailConfiguration);
        Services.AddScoped<IEmailService, EmailService>();

        return this;
    }
}
