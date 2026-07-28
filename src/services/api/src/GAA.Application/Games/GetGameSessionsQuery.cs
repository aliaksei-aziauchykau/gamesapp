using MediatR;

namespace GAA.Application.Games;

public sealed record GetGameSessionsQuery : IRequest<IReadOnlyList<GameSessionDto>>;
