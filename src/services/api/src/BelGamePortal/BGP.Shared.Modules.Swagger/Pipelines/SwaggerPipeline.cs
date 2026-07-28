using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.Swagger.Pipelines;

public static class SwaggerPipeline
{
    public static IApplicationBuilder UseSwaggerPipeline(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"Your API {description.ApiVersion}");
            }
        });

        return app;
    }
}
