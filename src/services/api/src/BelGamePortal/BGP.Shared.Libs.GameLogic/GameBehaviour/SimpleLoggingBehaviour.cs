using MediatR.Pipeline;
using Serilog;

namespace BGP.Shared.Libs.GameLogic;

public class SimpleLoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public SimpleLoggingBehavior(ILogger logger)
    {
        _logger = logger;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        // For server
        //_logger.Information("Mediator Request: {Name} {@Request}",
        //    requestName, request);
    }
}
