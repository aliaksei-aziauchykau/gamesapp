



using BGP.Emails.Integration.Contracts.Events;
using BGP.Platform.Application.Integration.Emails;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.ServiceBus;

namespace BGP.Emails.Integration.Consumers.Platform;

public class EmailSentIntegrationEventConsumer(IPlatformMicroservice microservice) : MediatorPropagateEventConsumer<EmailSentIntegrationEvent, EmailSendEvent>(microservice)
{
}
