using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class AddOrUpdateEntityCommand<TEntity, TKey> : IRequest<TEntity>
        where TEntity : class, IHasKey<TKey>
{
    public AddOrUpdateEntityCommand(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; set; }
}


public class AddOrUpdateEntityCommandHandler<TEntity, TKey> : IRequestHandler<AddOrUpdateEntityCommand<TEntity, TKey>, TEntity> where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public AddOrUpdateEntityCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(AddOrUpdateEntityCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.AddOrUpdateAsync(command.Entity, cancellationToken);

        return entity;
    }
}




