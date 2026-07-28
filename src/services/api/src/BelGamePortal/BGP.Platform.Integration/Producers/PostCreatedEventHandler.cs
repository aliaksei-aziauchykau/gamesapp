using BGP.Platform.Integration.Contracts.Events;
using BGP.Shared.Modules.ServiceBus.Producers;
using MassTransit;
using MediatR;

namespace BGP.Platform.Application.Modules.Posts;

public class PostCreatedEventHandler(IPublishEndpoint publishEndpoint, IMediator mediator) : ServiceBusPropagateEventProducer<PostCreatedEvent, PostCreatedIntegrationEvent>(publishEndpoint, mediator)
{
    public override Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {


        return base.Handle(notification, cancellationToken);
    }
}

