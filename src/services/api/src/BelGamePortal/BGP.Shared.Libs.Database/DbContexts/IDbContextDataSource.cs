using Microsoft.EntityFrameworkCore;

namespace BGP.Shared.Libs.Database.DbContexts;

public interface IDbContextDataSource
{
    IUnitOfWork UnitOfWork { get; }

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
