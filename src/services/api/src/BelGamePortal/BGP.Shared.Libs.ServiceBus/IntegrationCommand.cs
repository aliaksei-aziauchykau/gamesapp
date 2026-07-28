using MediatR;

namespace BGP.Shared.Libs.ServiceBus;

public abstract class IntegrationCommand : IRequest
{
    public abstract Uri DestinationAddress { get; protected set; }

    protected void SetDestinationAddress(Uri destinationAddress)
    {
        DestinationAddress = destinationAddress;
    }
}


public abstract class IntegrationCommand<TResponse> : IRequest<TResponse>
{
    public abstract Uri DestinationAddress { get; protected set; }

    protected void SetDestinationAddress(Uri destinationAddress)
    {
        DestinationAddress = destinationAddress;
    }
}
