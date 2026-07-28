using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoChangePlayerContextCommand : IRequest, IWithConnectionId
{
    public string GameId { get; set; }

    public string UserId { get; set; }

    public string Nickname { get; set; }

    public string ConnectionId { get; set; }
}

public class KrokoChangePlayerContextCommandHandler : IRequestHandler<KrokoChangePlayerContextCommand>
{
    private readonly IKrokoGameContext _gameContext;
    private readonly IKrokoGameHubContext _krokoGameHubContext;

    public KrokoChangePlayerContextCommandHandler(IKrokoGameContext gameContext, IKrokoGameHubContext krokoGameHubContext)
    {
        _gameContext = gameContext;
        _krokoGameHubContext = krokoGameHubContext;
    }

    public async Task Handle(KrokoChangePlayerContextCommand command, CancellationToken cancellationToken)
    {
        var playerContext = _gameContext.GetPlayerContextByUserId(command.UserId);

        if (command.Nickname != null)
        {
            playerContext.Nickname = command.Nickname;
        }

        await _krokoGameHubContext.SendAllGameContextAsync(_gameContext);
    }
}
