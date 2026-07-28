



using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.ServiceBus;
using BGP.Shared.Modules.AutoMapper.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BGP.Shared.Modules.ServiceBus;

public class MediatorPropagateEventConsumer<TIntegrationEvent, TEvent>(IProvideServiceProvider provider) : IMap<TIntegrationEvent, TEvent>, IConsumer<TIntegrationEvent>
where TIntegrationEvent : IntegrationEvent
where TEvent : INotification
{
    public virtual async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        Log.Information("Integration event", integrationEvent);

        using var scope = provider.Scope;

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var appEvent = await mediator.Send(new MapCommand<TIntegrationEvent, TEvent>(integrationEvent));

        await mediator.Publish(appEvent);
    }
}
