using BGP.Shared.Libs.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BGP.Shared.Libs.Database.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<IHasDomainEventCollection>()
            .Where(e => e.Entity.DomainEventCollection.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEventCollection.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.DomainEventCollection.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}
