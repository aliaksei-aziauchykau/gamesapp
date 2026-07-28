using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class GetEntityByIdCommand<TEntity, TKey> : IRequest<TEntity>
    where TEntity : IHasKey<TKey>
{
    public GetEntityByIdCommand(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; }
}

public class GetEntityByIdCommandHandler<TEntity, TKey> : IRequestHandler<GetEntityByIdCommand<TEntity, TKey>, TEntity>
    where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public GetEntityByIdCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(GetEntityByIdCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.GetByIdAsync(command.Id, cancellationToken);

        return entity;
    }
}

