using GAA.Application.Status;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace GAA.Application.Status.Handlers;

public class GetStatusQueryHandler(IHostEnvironment hostEnvironment) : IRequestHandler<GetStatusQuery, StatusDto>
{
    public Task<StatusDto> Handle(GetStatusQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new StatusDto(
            "GamesApp API",
            DateTimeOffset.UtcNow,
            hostEnvironment.EnvironmentName));
    }
}
