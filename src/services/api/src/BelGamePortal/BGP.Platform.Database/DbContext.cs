using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Database.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BGP.Platform.Database;

public class PlatformDbContext(DbContextOptions<PlatformDbContext> options, IMediator mediator, string schema = PlatformDbContext.Schema)
    : BaseDbContext<PlatformDbContext>(options, mediator, schema, typeof(PlatformDbContext).Assembly), IDbContextDataSource, IUnitOfWork
{
    public const string Schema = "platform";

    public IUnitOfWork UnitOfWork => this;

    public DbSet<Workspace> Workspaces => Set<Workspace>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectContainer> ProjectContainers => Set<ProjectContainer>();

    public DbSet<ProjectVisibility> ProjectVisibilities => Set<ProjectVisibility>();

    public DbSet<ParentContainerChildContainer> ParentContainerChildContainers => Set<ParentContainerChildContainer>();

    public DbSet<Container> Containers => Set<Container>();

    public DbSet<Post> Posts => Set<Post>();

    public DbSet<Event> Events => Set<Event>();
}
