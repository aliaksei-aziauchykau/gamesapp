


using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.Common.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
namespace BGP.Shared.Modules.Authentication;

public class AuthenticationModule : DIModule<AuthenticationModule>
{
    public AuthenticationModule AddAuthentication(AuthenticationModuleConfiguration configuration)
    {
        var authority = configuration.Authority.ThrowExceptionIfNull();
        var audience = configuration.Audience.ThrowExceptionIfNull();

        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authority;
            options.Audience = audience;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
            };
        });

        return this;
    }
}
