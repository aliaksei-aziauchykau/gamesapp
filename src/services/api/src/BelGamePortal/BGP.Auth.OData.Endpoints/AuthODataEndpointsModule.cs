using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BGP.Auth.OData.Endpoints;

public class AuthODataEndpointsModule
{
    private IServiceCollection? _services;

    private IServiceCollection Services => _services ?? throw new InvalidOperationException("Services not set");


    public static AuthODataEndpointsModule Create()
    {
        return new AuthODataEndpointsModule();
    }

    public AuthODataEndpointsModule AddServiceCollection(IServiceCollection services)
    {
        this._services = services;

        return this;
    }

    public AuthODataEndpointsModule AddEndpoints(IConfigurationManager configurationManager, IHostEnvironment environment)
    {

        Services.AddControllers()
                   .ConfigureApplicationPartManager(apm =>
                       apm.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly())));

        return this;
    }
}
