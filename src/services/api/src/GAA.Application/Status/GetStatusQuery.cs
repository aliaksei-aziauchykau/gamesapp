using MediatR;

namespace GAA.Application.Status;

public record GetStatusQuery : IRequest<StatusDto>;

public record StatusDto(string Application, DateTimeOffset UtcNow, string EnvironmentName);
