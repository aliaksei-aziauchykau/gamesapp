using BGP.Shared.Libs.GameLogic.Interfaces;
using MediatR;

namespace BGP.Shared.Libs.GameLogic;

public class GameContainerDisconnectCommand : IRequest, IWithConnectionId
{
    public string UserId { get; set; }

    public string GameId { get; set; }

    public string ConnectionId { get; set; }
}

public class GameContainerDisconnectCommandHandler : IRequestHandler<GameContainerDisconnectCommand>
{
    private readonly IGameContainerHubContext _hubContext;

    public GameContainerDisconnectCommandHandler(IGameContainerHubContext hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Handle(GameContainerDisconnectCommand command, CancellationToken cancellationToken)
    {
        await _hubContext.RemoveConnectionFromGameGroupAsync(command.ConnectionId, command.GameId, cancellationToken);
        await _hubContext.RemoveConnectionFromUserGroupAsync(command.ConnectionId, command.GameId, command.UserId, cancellationToken);
    }
}
