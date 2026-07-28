using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.Identity;

public class IdentityModule
{
    private IServiceCollection services;

    public static IdentityModule Create()
    {
        return new IdentityModule();
    }

    public IdentityModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public IdentityModule AddIdentity(Assembly assembly)
    {
        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        });

        return this;
    }
}
