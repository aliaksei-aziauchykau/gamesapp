using System.Data;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BGP.Shared.Libs.Database.Extensions;

namespace BGP.Shared.Libs.Database.DbContexts;

public abstract class BaseDbContext<T> : DbContext where T : DbContext
{
    protected readonly IMediator _mediator;

    private readonly string Schema;
    private readonly Assembly Assembly;

    public BaseDbContext(DbContextOptions<T> options, IMediator mediator, string schema, Assembly assembly)
        : base(options)
    {
        _mediator = mediator;
        Schema = schema;
        Assembly = assembly;
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(Schema);
        builder.ApplyConfigurationsFromAssembly(Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
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
