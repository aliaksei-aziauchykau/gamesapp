using BGP.Emails.Domain.Entities;
using BGP.Shared.Libs.Database.Configuration;
using BGP.Shared.Libs.Database.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BGP.Emails.Database;
public class EmailDbContext(DbContextOptions<EmailDbContext> options, IMediator mediator, string schema = EmailDbContext.Schema)
    : BaseDbContext<EmailDbContext>(options, mediator, schema, typeof(EmailDbContext).Assembly), IDbContextDataSource, IUnitOfWork
{
    public const string Schema = "emails";

    public IUnitOfWork UnitOfWork => this;

    public DbSet<Email> Emails { get; set; }
}
