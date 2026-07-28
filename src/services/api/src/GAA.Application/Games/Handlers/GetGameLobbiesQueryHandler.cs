using MediatR;

namespace GAA.Application.Games.Handlers;

public sealed class GetGameLobbiesQueryHandler(IGamesReadRepository repository)
    : IRequestHandler<GetGameLobbiesQuery, IReadOnlyList<GameLobbyDto>>
{
    public Task<IReadOnlyList<GameLobbyDto>> Handle(
        GetGameLobbiesQuery request,
        CancellationToken cancellationToken) =>
        repository.GetLobbiesAsync(cancellationToken);
}
