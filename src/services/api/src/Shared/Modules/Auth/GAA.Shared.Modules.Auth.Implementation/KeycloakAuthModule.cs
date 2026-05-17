using GAA.Shared.Modules.Auth.Interfaces;
using GAA.Shared.Modules.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Shared.Modules.Auth.Implementation;

public class KeycloakAuthModule : DIModule<KeycloakAuthModule>, IAuthModule
{
    public KeycloakAuthModule AddKeycloakJwtBearer(KeycloakAuthConfiguration configuration)
    {
        var authority = configuration.Authority.TrimEnd('/');

        Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = configuration.Audience;
                options.RequireHttpsMetadata = configuration.RequireHttpsMetadata;
                if (!string.IsNullOrWhiteSpace(configuration.Audience))
                {
                    options.TokenValidationParameters.ValidAudience = configuration.Audience;
                }

                options.TokenValidationParameters.NameClaimType = "preferred_username";
                options.TokenValidationParameters.RoleClaimType = "roles";
            });

        Services.AddAuthorization();

        return this;
    }

    public void Configure()
    {
    }
}
