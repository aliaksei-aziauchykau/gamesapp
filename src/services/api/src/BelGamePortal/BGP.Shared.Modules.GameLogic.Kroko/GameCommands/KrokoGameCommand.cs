using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoGameCommand : GameCommand, IRequest
{
}

public class KrokoGameCommandHandler : IRequestHandler<KrokoGameCommand>
{
    private readonly IKrokoGameContext _krokoGameContext;

    public KrokoGameCommandHandler(IKrokoGameContext krokoGameContext)
    {
        _krokoGameContext = krokoGameContext;
    }

    public Task Handle(KrokoGameCommand request, CancellationToken cancellationToken)
    {
        _krokoGameContext.Layout.ShowBoard();

        return Task.CompletedTask;
    }
}
