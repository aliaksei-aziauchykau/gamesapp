using MediatR;

namespace GAA.Application.Games;

public sealed record GetGameDefinitionsQuery : IRequest<IReadOnlyList<GameDefinitionDto>>;
