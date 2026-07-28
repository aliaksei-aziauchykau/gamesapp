using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Libs.CrudServices.Interfaces.Services;

public interface IGetEntitiesService<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    Task<IQueryable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken = default);
}
