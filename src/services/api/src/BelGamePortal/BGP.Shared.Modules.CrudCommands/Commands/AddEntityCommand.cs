using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class AddEntityCommand<TEntity, TKey> : IRequest<TEntity> where TEntity : class, IHasKey<TKey>
{
    public AddEntityCommand(TEntity entity)
    {
        Entity = entity;
    }

    public TEntity Entity { get; set; }
}

public class AddEntityCommandHandler<TEntity, TKey> : IRequestHandler<AddEntityCommand<TEntity, TKey>, TEntity> where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public AddEntityCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(AddEntityCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.AddAsync(command.Entity, cancellationToken);

        return entity;
    }
}



