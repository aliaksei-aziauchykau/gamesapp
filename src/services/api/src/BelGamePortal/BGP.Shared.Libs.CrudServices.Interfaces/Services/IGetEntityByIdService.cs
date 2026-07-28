using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IGetEntityByIdService<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

}
