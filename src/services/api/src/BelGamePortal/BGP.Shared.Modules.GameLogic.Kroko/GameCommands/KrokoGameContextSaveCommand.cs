using AutoMapper;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.Commands;

public class KrokoGameContextSaveCommand : GameCommand, IRequest
{
}

public class KrokoGameContextSaveCommandHandler : IRequestHandler<KrokoGameContextSaveCommand>
{
    // private readonly IEntityApiClient<KrokoGame, string> _entityApiClient;

    private readonly IKrokoGameContext _krokoGameContext;

    public KrokoGameContextSaveCommandHandler(
        IKrokoGameContext krokoGameContext)
    {
        _krokoGameContext = krokoGameContext;
    }

    public async Task Handle(KrokoGameContextSaveCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // var krokoGame = _mapper.Map<KrokoGameCreateDto>(_krokoGameContext);

            // await _entityApiClient.AddAsync(krokoGame);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
