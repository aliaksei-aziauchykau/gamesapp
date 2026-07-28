using BGP.Games.Domain.Entities;
using BGP.Games.Integration.Events;
using BGP.Shared.Libs.Common.Helpers;
using BGP.Shared.Modules.CrudCommands.Repositories;
using BGP.Shared.Modules.CrudCommands.Services;

namespace BGP.Games.Application.Modules.Games;

public class GamesCrudService : CrudService<Game, Guid>
{

    public GamesCrudService(IRepository<Game, Guid> repository) : base(repository)
    {
    }

    public override async Task<Game> AddAsync(Game entity, CancellationToken cancellationToken = default)
    {
        entity.Code = RandomSymbolHelper.GetRandomSymbols();

        var game = await base.AddAsync(entity, cancellationToken);

        return game;
    }

}
