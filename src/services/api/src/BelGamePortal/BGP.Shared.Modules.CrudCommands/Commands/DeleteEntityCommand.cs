using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Shared.Modules.CrudCommands.Commands;

public record DeleteEntityCommand<TEntity, TKey>(TKey Id) : IRequest<TEntity> where TEntity : class, IHasKey<TKey>
{
}

public class DeleteEntityCommandHandler<TEntity, TKey> : IRequestHandler<DeleteEntityCommand<TEntity, TKey>, TEntity> where TEntity : class, IHasKey<TKey>, new()
{
    private readonly ICrudService<TEntity, TKey> _crudService;

    public DeleteEntityCommandHandler(ICrudService<TEntity, TKey> crudService)
    {
        _crudService = crudService;
    }

    public async Task<TEntity> Handle(DeleteEntityCommand<TEntity, TKey> command, CancellationToken cancellationToken)
    {
        var entity = await _crudService.DeleteAsync(command.Id, cancellationToken);

        return entity;
    }
}



