using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace BGP.Emails.OData.Endpoints;

public class EmailsODataEndpointsModule
{
    private IServiceCollection services;

    public static EmailsODataEndpointsModule Create()
    {
        return new EmailsODataEndpointsModule();
    }

    public EmailsODataEndpointsModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public EmailsODataEndpointsModule AddEndpoints(IConfigurationManager configurationManager, IHostEnvironment environment)
    {
        services.AddControllers()
                   .ConfigureApplicationPartManager(apm =>
                       apm.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly())));


        return this;
    }
}
