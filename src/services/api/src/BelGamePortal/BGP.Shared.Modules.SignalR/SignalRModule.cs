
using BGP.Shared.Libs.Common;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.SignalR;

public class SignalRModule : DIModule<SignalRModule>
{
    public SignalRModule AddSignalR()
    {
        Services
            .AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 6500000;
                options.DisableImplicitFromServicesParameters = true;
            });


        return this;
    }
}
