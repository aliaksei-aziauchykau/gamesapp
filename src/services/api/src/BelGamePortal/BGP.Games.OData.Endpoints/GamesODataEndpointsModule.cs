using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BGP.Games.OData.Endpoints;

public class GamesODataEndpointsModule
{
    private IServiceCollection services;

    public static GamesODataEndpointsModule Create()
    {
        return new GamesODataEndpointsModule();
    }

    public GamesODataEndpointsModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public GamesODataEndpointsModule AddEndpoints(IConfigurationManager configurationManager, IHostEnvironment environment)
    {

        services.AddControllers()
                   .ConfigureApplicationPartManager(apm =>
                       apm.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly())));

        return this;
    }
}
