using BGP.Games.Domain.Entities;
using BGP.Games.Domain.Entities.Games.Kroko;
using BGP.Shared.Libs.Database.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BGP.Games.Database;

public class GamesDbContext(DbContextOptions<GamesDbContext> options, IMediator mediator, string schema = GamesDbContext.Schema)
    : BaseDbContext<GamesDbContext>(options, mediator, schema, typeof(GamesDbContext).Assembly), IDbContextDataSource, IUnitOfWork
{
    public const string Schema = "games";

    public IUnitOfWork UnitOfWork => this;

    public DbSet<Game> Games => Set<Game>();

    public DbSet<Player> Players => Set<Player>();

    public DbSet<KrokoWord> KrokoWords => Set<KrokoWord>();

    public DbSet<KrokoWordLanguage> KrokoWordLanguages => Set<KrokoWordLanguage>();

    public DbSet<GameDefinition> GameDefinitions => Set<GameDefinition>();
}
