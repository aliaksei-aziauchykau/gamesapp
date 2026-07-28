


using BGP.Emails.Application.Integration.Platform;
using BGP.Emails.Microservice.Interfaces;
using BGP.Platform.Integration.Contracts.Events;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Modules.ServiceBus;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BGP.Emails.Integration.Consumers.Platform;

public class PostCreatedIntegrationEventConsumer(IEmailsMicroservice microservice) : MediatorPropagateEventConsumer<PostCreatedIntegrationEvent, PostCreatedEvent>(microservice)
{
    public override Task Consume(ConsumeContext<PostCreatedIntegrationEvent> context)
    {
        return base.Consume(context);
    }
}
