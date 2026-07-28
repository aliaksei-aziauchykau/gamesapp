using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IDeleteEntityService<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
