using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class GetEntitiesCommand<TEntity, TKey> : IRequest<IQueryable<TEntity>>
    where TEntity : IHasKey<TKey>
{ }

public class GetEntitiesCommandHandler<TEntity, TKey> : IRequestHandler<GetEntitiesCommand<TEntity, TKey>, IQueryable<TEntity>> where TEntity : class, IHasKey<TKey>, new()
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public GetEntitiesCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<IQueryable<TEntity>> Handle(GetEntitiesCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var queryable = await _crudService.GetAsync(cancellationToken);

        return queryable;
    }
}

