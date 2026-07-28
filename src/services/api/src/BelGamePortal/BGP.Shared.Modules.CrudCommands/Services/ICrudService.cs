using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Modules.CrudCommands.Services;

public interface ICrudService<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
{
    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IQueryable<TEntity?>> GetByIdQueryAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TEntity> AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> PatchAsync(TKey id, IDelta<TEntity> patcher, CancellationToken cancellationToken = default);

    Task<TEntity> DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
}
