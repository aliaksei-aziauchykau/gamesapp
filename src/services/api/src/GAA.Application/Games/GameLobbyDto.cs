namespace GAA.Application.Games;

public sealed record GameLobbyDto(
    int Id,
    int GameDefinitionId,
    string Name,
    bool IsOpen,
    DateTimeOffset CreatedAt);
