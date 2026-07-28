using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IPatchEntityService<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task<TEntity> PatchAsync(TKey id, IDelta<TEntity> patcher, CancellationToken cancellationToken = default);
}
