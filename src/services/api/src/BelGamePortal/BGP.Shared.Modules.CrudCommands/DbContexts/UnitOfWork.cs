using System.Data;
using BGP.Shared.Libs.Database.DbContexts;

namespace BGP.Shared.Modules.CrudCommands.DbContexts;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContextDataSource dbContext;

    public UnitOfWork(IDbContextDataSource dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
