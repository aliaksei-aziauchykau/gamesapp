using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IUpdateEntityService<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

}
