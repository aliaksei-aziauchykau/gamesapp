using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BGP.Shared.Modules.DotNetEnv;

public class DotNetEnvModule
{
    public static DotNetEnvModule Create()
    {
        return new DotNetEnvModule();
    }

    public DotNetEnvModule ReadEnvFiles(IConfigurationManager configurationManager, IHostEnvironment environment, string basePath = "./", string prefix = "")
    {

        configurationManager.AddDotNetEnvVariables(
                environmentName: environment.EnvironmentName,
                basePath: basePath,
                prefix: prefix);

        return this;
    }

    public DotNetEnvModule ReadEnvFiles(IConfigurationManager configurationManager, string environmentName, string basePath = "./", string prefix = "")
    {

        configurationManager.AddDotNetEnvVariables(
                environmentName: environmentName,
                basePath: basePath,
                prefix: prefix);

        return this;
    }
}
