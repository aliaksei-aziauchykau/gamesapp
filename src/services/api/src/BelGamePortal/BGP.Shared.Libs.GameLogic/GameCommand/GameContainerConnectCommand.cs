
using BGP.Shared.Libs.GameLogic.Interfaces;
using MediatR;

namespace BGP.Shared.Libs.GameLogic;

public class GameContainerConnectCommand : IRequest, IWithConnectionId
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string ConnectionId { get; set; }
}

public class GameContainerConnectCommandHandler : IRequestHandler<GameContainerConnectCommand>
{
    private readonly IGameContainerHubContext _hubContext;

    public GameContainerConnectCommandHandler(IGameContainerHubContext hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(GameContainerConnectCommand command, CancellationToken cancellationToken)
    {
        await _hubContext.AddConnectionToGameGroupAsync(command.ConnectionId, command.GameId, cancellationToken);
        await _hubContext.AddConnectionToUserGroupAsync(command.ConnectionId, command.GameId, command.UserId, cancellationToken);
    }
}
