namespace GAA.Application.Games;

public sealed record GameSessionDto(
    int Id,
    int GameDefinitionId,
    string Title,
    string Status,
    DateTimeOffset CreatedAt);
