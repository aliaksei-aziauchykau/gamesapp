using BGP.Emails.Application.Modules.Emails.Commands;
using MediatR;

namespace BGP.Emails.Application.Integration.Platform;

// Define the event
public class PostCreatedEvent : INotification
{
}

// Define the event handler
public class PostCreatedEventHandler(IMediator mediator) : INotificationHandler<PostCreatedEvent>
{
    public async Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
    {
        await mediator.Send(new SendEmailCommand());
    }
}
