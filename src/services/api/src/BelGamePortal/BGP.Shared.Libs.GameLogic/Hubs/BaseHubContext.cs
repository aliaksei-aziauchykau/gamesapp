using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Libs.GameLogic.Hubs;

public abstract class BaseHubContext<T> : IHubContext<T> where T : Hub
{
    protected readonly IHubContext<T> hubContext;

    public BaseHubContext(IHubContext<T> hubContext)
    {
        this.hubContext = hubContext;
    }

    public IHubClients Clients => this.hubContext.Clients;

    public IGroupManager Groups => this.hubContext.Groups;
}
