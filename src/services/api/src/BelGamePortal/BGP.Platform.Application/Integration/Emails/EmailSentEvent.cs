using MediatR;

namespace BGP.Platform.Application.Integration.Emails;


public class EmailSendEvent : INotification
{
}

public class EmailSendEventHandler(IMediator mediator) : INotificationHandler<EmailSendEvent>
{
    public Task Handle(EmailSendEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
