using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class GetEntityByIdQueryCommand<TEntity, TKey> : IRequest<IQueryable<TEntity?>>
    where TEntity : IHasKey<TKey>
{
    public GetEntityByIdQueryCommand(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; }
}

public class GetEntityByIdQueryCommandHandler<TEntity, TKey> : IRequestHandler<GetEntityByIdQueryCommand<TEntity, TKey>, IQueryable<TEntity?>>
    where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public GetEntityByIdQueryCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<IQueryable<TEntity?>> Handle(GetEntityByIdQueryCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var query = await _crudService.GetByIdQueryAsync(command.Id, cancellationToken);

        return query;
    }
}

