

using DotNetEnv.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddDotNetEnvVariables(this IConfigurationBuilder builder, string environmentName = "", string basePath = "./", string prefix = "")
    {
        var postfix = ".env";
        var prefixWithDot = string.IsNullOrEmpty(prefix) ? string.Empty : $".{prefix}";
        var environmentWithDot = string.IsNullOrEmpty(environmentName) ? string.Empty : $".{environmentName}";
        var baseEnv = $"{basePath}{prefixWithDot}{postfix}";
        var envEnv = $"{basePath}{environmentWithDot}{prefixWithDot}{postfix}";
        builder
             .AddDotNetEnv(baseEnv)
             .AddDotNetEnv(envEnv);

        return builder;
    }
}
