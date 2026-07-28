using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.ServiceBus;
using BGP.Shared.Modules.AutoMapper.Commands;
using MassTransit;
using MediatR;

namespace BGP.Shared.Modules.ServiceBus.Producers;

public class ServiceBusPropagateEventProducer<TEvent, TIntegrationEvent>(IPublishEndpoint publishEndpoint, IMediator mediator) : INotificationHandler<TEvent>, IMap<TEvent, TIntegrationEvent>
where TEvent : INotification
where TIntegrationEvent : IntegrationEvent
{
    public virtual async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = await mediator.Send(new MapCommand<TEvent, TIntegrationEvent>(notification), cancellationToken);

        await publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
