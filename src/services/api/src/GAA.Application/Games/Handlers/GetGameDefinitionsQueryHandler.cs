using MediatR;

namespace GAA.Application.Games.Handlers;

public sealed class GetGameDefinitionsQueryHandler(IGamesReadRepository repository)
    : IRequestHandler<GetGameDefinitionsQuery, IReadOnlyList<GameDefinitionDto>>
{
    public Task<IReadOnlyList<GameDefinitionDto>> Handle(
        GetGameDefinitionsQuery request,
        CancellationToken cancellationToken) =>
        repository.GetGameDefinitionsAsync(cancellationToken);
}
