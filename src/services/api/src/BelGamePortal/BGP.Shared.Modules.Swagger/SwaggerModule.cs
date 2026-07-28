



using Asp.Versioning.ApiExplorer;
using BGP.Shared.Libs.Common;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BGP.Shared.Modules.Swagger;

public class SwaggerModule : DIModule<SwaggerModule>
{
    public SwaggerModule AddSwaggerServices(SwaggerModuleConfiguration configuration)
    {

        var tokenUrl = configuration.TokenUrl ?? throw new InvalidOperationException("Token Url not found");

        Services.AddSwaggerGen(options =>
        {
            var provider = Services.BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Your API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                });
            }

            options.SchemaFilter<DeltaSchemaFilter>();
            options.CustomSchemaIds(type => type.ToString());
            options.CustomOperationIds(e => $"{e.RelativePath}_{e.ActionDescriptor.RouteValues["action"]}_{string.Join("", e.ParameterDescriptions.Select(x => x.Name))}");

            options.AddSecurityDefinition("Auth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {

                        TokenUrl = new Uri(configuration.TokenUrl),
                    }
                },
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Auth2",
                        }

                    },
                    new string[] { }
                }
            });
        });

        return this;
    }

    public class DeltaSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(Delta<>))
            {
                var entityType = context.Type.GenericTypeArguments[0];
                var properties = entityType.GetProperties();

                schema.Properties = properties.ToDictionary(
                    prop => prop.Name.ToLower(),
                    prop => context.SchemaGenerator.GenerateSchema(prop.PropertyType, context.SchemaRepository)
                );
            }
        }
    }
}
