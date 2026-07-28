using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class GetAllEntitiesCommand<TEntity, TKey> : IRequest<IList<TEntity>>
    where TEntity : class, IHasKey<TKey>
{ }

public class GetAllEntitiesCommandHandler<TEntity, TKey> : IRequestHandler<GetAllEntitiesCommand<TEntity, TKey>, IList<TEntity>> where TEntity : class, IHasKey<TKey>, new()
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public GetAllEntitiesCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<IList<TEntity>> Handle(GetAllEntitiesCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entities = await _crudService.GetAllAsync(cancellationToken);

        return entities;
    }
}
