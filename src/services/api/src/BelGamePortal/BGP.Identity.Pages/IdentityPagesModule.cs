using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace BGP.Identity.Pages;

public class IdentityPagesModule
{
    private IServiceCollection services;

    public static IdentityPagesModule Create()
    {
        return new IdentityPagesModule();
    }

    public IdentityPagesModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public IdentityPagesModule AddPages(IConfigurationManager configurationManager)
    {

        services.AddRazorPages()
                   .ConfigureApplicationPartManager(apm =>
                       apm.ApplicationParts.Add(new AssemblyPart(Assembly.GetExecutingAssembly())));

        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "BGP.Identity.Pages");
        configurationManager.AddJsonFile(embeddedProvider, "appsettings.identity.json", optional: false, reloadOnChange: true);

        return this;
    }

    public IdentityPagesModule AddEmailSender()
    {
        services.AddSingleton<IEmailSender, EmailSender>();

        return this;
    }
}

public static class IdentityPagesModuleExtensions
{
    public static IApplicationBuilder UseIdentityModuleStaticFiles(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "..\\BGP.Identity.Pages", "wwwroot")),
            RequestPath = "/Identity"
        });

        return app;
    }
}

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}
