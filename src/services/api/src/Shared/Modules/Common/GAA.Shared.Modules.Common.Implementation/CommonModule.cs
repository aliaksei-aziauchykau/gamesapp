using GAA.Shared.Modules.Common.Interfaces;
using GAA.Shared.Modules.Common.Interfaces.Services;
using GAA.Shared.Modules.Common.Implementation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GAA.Shared.Modules.Common.Implementation;

public class CommonModule : DIModule<CommonModule>, ICommonModule
{
    public CommonModule AddCommon(CommonModuleConfiguration? configuration = null)
    {
        var config = configuration ?? new CommonModuleConfiguration();

        if (config.EnableUtilities)
        {
            Services.AddSingleton<IUtilityService, UtilityService>();
        }

        return this;
    }

    public void Configure()
    {
    }
}
