using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BGP.Platform.OData.Endpoints;

public class PlatformODataEndpointsModule
{
    private IServiceCollection services;

    public static PlatformODataEndpointsModule Create()
    {
        return new PlatformODataEndpointsModule();
    }

    public PlatformODataEndpointsModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public PlatformODataEndpointsModule AddEndpoints(IConfigurationManager configurationManager, IHostEnvironment environment)
    {

        services.AddControllers()
                   .ConfigureApplicationPartManager(apm =>
                       apm.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly())));

        return this;
    }
}
