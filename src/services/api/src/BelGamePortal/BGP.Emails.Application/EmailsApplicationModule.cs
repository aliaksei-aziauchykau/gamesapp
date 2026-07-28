using System.Reflection;
using BGP.Shared.Modules.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Emails.Application;

public class EmailsApplicationModule
{
    private IServiceCollection services;

    public static EmailsApplicationModule Create()
    {
        return new EmailsApplicationModule();
    }

    public EmailsApplicationModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EmailsApplicationModule AddMediator()
    {
        MediatorModule
            .Create()
            .AddServiceCollection(services)
            .AddMediator(Assembly.GetExecutingAssembly());

        return this;
    }
}
