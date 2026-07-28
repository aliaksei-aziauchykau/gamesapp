using MediatR;

namespace GAA.Application.Games;

public sealed record GetGameLobbiesQuery : IRequest<IReadOnlyList<GameLobbyDto>>;
