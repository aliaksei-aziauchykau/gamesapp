using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public class PatchEntityCommand<TEntity, TKey> : IRequest<TEntity> where TEntity : class, IHasKey<TKey>
{
    public PatchEntityCommand(TKey id, IDelta<TEntity> patcher)
    {
        Id = id;
        Patcher = patcher;
    }

    public IDelta<TEntity> Patcher { get; set; }

    public TKey Id { get; set; }
}

public class PatchEntityCommandHandler<TEntity, TKey> : IRequestHandler<PatchEntityCommand<TEntity, TKey>, TEntity> where TEntity : class, IHasKey<TKey>
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public PatchEntityCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(PatchEntityCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.PatchAsync(command.Id, command.Patcher, cancellationToken);

        return entity;
    }
}

