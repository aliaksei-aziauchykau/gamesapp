using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class UpdateEntityCommand<TEntity, TKey> : IRequest<TEntity> where TEntity : class, IHasKey<TKey>
{
    public UpdateEntityCommand(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; set; }
}

public class UpdateEntityCommandHandler<TEntity, TKey> : IRequestHandler<UpdateEntityCommand<TEntity, TKey>, TEntity> where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public UpdateEntityCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(UpdateEntityCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.UpdateAsync(command.Entity, cancellationToken);

        return entity;
    }
}
