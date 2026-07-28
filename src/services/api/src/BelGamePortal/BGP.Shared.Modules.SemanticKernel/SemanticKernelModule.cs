using BGP.Shared.Libs.SemanticKernel.Configuration;
using BGP.Shared.Libs.SemanticKernel.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace BGP.Shared.Modules.SemanticKernel;

public class SemanticKernelModule
{
    private IServiceCollection? services;

    private IServiceCollection Services => services ?? throw new InvalidOperationException("Service collection not set");

    public static SemanticKernelModule Create()
    {
        return new SemanticKernelModule();
    }

    public SemanticKernelModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;

    }
    public SemanticKernelModule AddServices(ISemanticKernelOllamaConfiguration configuration)
    {

        // #pragma warning disable SKEXP0070
        Services.AddOllamaChatCompletion(configuration.Model, configuration.Uri);
        // #pragma warning restore CSXXXX

        Services.AddSingleton(new EmailPlugin());
        Services.AddSingleton(new AuthorEmailPlannerPlugin());
        Services.AddSingleton(new LightsPlugin());
        Services.AddSingleton(new ClipboardAccessPlugin());

        Services.AddSingleton<KernelPluginCollection>((serviceProvider) =>
        [
            KernelPluginFactory.CreateFromObject(serviceProvider.GetRequiredService<EmailPlugin>()),
            KernelPluginFactory.CreateFromObject(serviceProvider.GetRequiredService<AuthorEmailPlannerPlugin>()),
            KernelPluginFactory.CreateFromObject(serviceProvider.GetRequiredService<LightsPlugin>()),
            KernelPluginFactory.CreateFromObject(serviceProvider.GetRequiredService<ClipboardAccessPlugin>())
        ]);

        Services.AddTransient((serviceProvider) =>
        {
            KernelPluginCollection pluginCollection = serviceProvider.GetRequiredService<KernelPluginCollection>();

            var kernel = new Kernel(serviceProvider, pluginCollection);
            return kernel;
        });

        return this;
    }

    public SemanticKernelModule WithMondoDbVectors(string connectionString, string databaseName)
    {
        Services.AddMongoDBVectorStore(connectionString, databaseName);

        return this;
    }
}

