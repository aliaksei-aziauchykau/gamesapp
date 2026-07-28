using BGP.Shared.Libs.Database.DbContexts;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Shared.Modules.CrudCommands.DataSources;

public interface IDataSource<TEntity, TKey> where TEntity : class, IHasKey<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<IQueryable<TEntity?>> GetByIdQueryAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default);

    Task<TEntity> PatchAsync(IDelta<TEntity> delta, TEntity entityToPatch, CancellationToken cancellationToken = default);
}
