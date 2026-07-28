namespace GAA.Application.Games;

public interface IGamesReadRepository
{
    Task<IReadOnlyList<GameDefinitionDto>> GetGameDefinitionsAsync(CancellationToken cancellationToken);

    Task<GameDefinitionDto?> GetGameDefinitionByCodeAsync(string code, CancellationToken cancellationToken);

    Task<IReadOnlyList<GameSessionDto>> GetGameSessionsAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<GameLobbyDto>> GetLobbiesAsync(CancellationToken cancellationToken);
}
