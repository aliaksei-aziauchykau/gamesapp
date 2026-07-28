


using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
namespace BGP.Shared.Modules.Cors;

public class CorsModule : DIModule<CorsModule>
{
    public CorsModule AddCorsServices(CorsModuleConfiguration configuration)
    {

        Services.AddCors(options =>
        {
            var clientOriginAll = configuration.ClientOrigins.ThrowExceptionIfNull();
            var clientOrigins = clientOriginAll.Split(',');
            options.AddDefaultPolicy(
                policy =>
                {
                    policy
                        .WithOrigins(clientOrigins)
                        // .WithHeaders([
                        //     HeaderNames.ContentType,
                        //     HeaderNames.Authorization,
                        // ])
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
                });
        });


        return this;
    }
}
