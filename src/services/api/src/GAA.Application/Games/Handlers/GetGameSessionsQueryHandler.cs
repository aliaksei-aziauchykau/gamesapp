using MediatR;

namespace GAA.Application.Games.Handlers;

public sealed class GetGameSessionsQueryHandler(IGamesReadRepository repository)
    : IRequestHandler<GetGameSessionsQuery, IReadOnlyList<GameSessionDto>>
{
    public Task<IReadOnlyList<GameSessionDto>> Handle(
        GetGameSessionsQuery request,
        CancellationToken cancellationToken) =>
        repository.GetGameSessionsAsync(cancellationToken);
}
