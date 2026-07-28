

using BGP.Shared.Libs.GameLogic.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BGP.Shared.Modules.GameLogic.Hubs.Contexts;

public class GameContainerHubContext : GameHubContext<GameContainerHub>, IGameContainerHubContext
{
    public GameContainerHubContext(IHubContext<GameContainerHub> hubContext) : base(hubContext)
    {
    }
}
