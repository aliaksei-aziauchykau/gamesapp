using BGP.Shared.Libs.Common.Helpers;
using BGP.Shared.Libs.Database.DbContexts;
using BGP.Shared.Libs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGP.Shared.Modules.CrudCommands.DataSources;

public class DbContextDataSource<TDbContext, TEntity, TKey> : IDataSource<TEntity, TKey>
    where TEntity : class, IHasKey<TKey>
    where TDbContext : IDbContextDataSource
{
    private readonly TDbContext _dbContext;

    public IUnitOfWork UnitOfWork => _dbContext.UnitOfWork;

    private DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

    public DbContextDataSource(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await DbSet.ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var equalsExpression = QueryHelper.GetEqualExpression<TEntity, TKey>(id);
        var entity = await DbSet.AsNoTracking().FirstOrDefaultAsync(equalsExpression, cancellationToken);
        return entity;
    }

    public Task<IQueryable<TEntity>> GetByIdQueryAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var equalsExpression = QueryHelper.GetEqualExpression<TEntity, TKey>(id);
        var query = DbSet.AsNoTracking().Where(equalsExpression);
        return Task.FromResult(query);
    }


    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.FromResult(entity);
    }

    public Task<IQueryable<TEntity>> GetAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(DbSet as IQueryable<TEntity>);
    }

    public Task<TEntity> PatchAsync(IDelta<TEntity> delta, TEntity entityToPatch, CancellationToken cancellationToken = default)
    {
        delta.Patch(entityToPatch);

        return Task.FromResult(entityToPatch);
    }
}
