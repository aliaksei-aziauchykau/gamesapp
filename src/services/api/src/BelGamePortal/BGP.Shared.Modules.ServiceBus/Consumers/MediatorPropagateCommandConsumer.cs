using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.Microservice;
using BGP.Shared.Libs.ServiceBus;
using BGP.Shared.Modules.AutoMapper.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BGP.Shared.Modules.ServiceBus;

public class MediatorPropagateCommandConsumer<TIntegrationCommand, TCommand>(IProvideServiceProvider provider) : IMap<TIntegrationCommand, TCommand>,
IConsumer<TIntegrationCommand>
where TIntegrationCommand : IntegrationCommand
where TCommand : IBaseRequest
{
    public virtual async Task Consume(ConsumeContext<TIntegrationCommand> context)
    {
        var integrationCommand = context.Message;

        Log.Information("Integration command", integrationCommand);

        using var scope = provider.Scope;

        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var appEvent = await mediator.Send(new MapCommand<TIntegrationCommand, TCommand>(integrationCommand));

        await mediator.Publish(appEvent);
    }
}
