using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Libs.ServiceBus;
using BGP.Shared.Modules.AutoMapper.Commands;
using MassTransit;
using MediatR;

namespace BGP.Shared.Modules.ServiceBus;

public class ServiceBusPropagateCommandProducer<TCommand, TIntegrationCommand>(ISendEndpointProvider sendEndpointProvider, IMediator mediator) : IRequestHandler<TCommand>, IMap<TCommand, TIntegrationCommand>
where TCommand : IRequest
where TIntegrationCommand : IntegrationCommand
{
    public virtual async Task Handle(TCommand command, CancellationToken cancellationToken)
    {

        var integrationCommand = await mediator.Send(new MapCommand<TCommand, TIntegrationCommand>(command), cancellationToken);

        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(integrationCommand.DestinationAddress);

        await sendEndpoint.Send(integrationCommand, cancellationToken);
    }
}
