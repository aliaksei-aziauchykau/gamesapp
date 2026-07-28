using GAA.Application.Games;
using Npgsql;

namespace GAA.Infrastructure.Data;

public sealed class GamesReadRepository : IGamesReadRepository
{
    private readonly string? _connectionString;

    public GamesReadRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("API_DATABASE_CONNECTION_STRING");
    }

    public async Task<IReadOnlyList<GameDefinitionDto>> GetGameDefinitionsAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
            return GetFallbackDefinitions();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT id, code, name, description, image_url, image_alt, game_page_app_url
            FROM game_definitions
            ORDER BY name
            """;

        await using var cmd = new NpgsqlCommand(sql, connection);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<GameDefinitionDto>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new GameDefinitionDto(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.IsDBNull(3) ? null : reader.GetString(3),
                reader.IsDBNull(4) ? null : reader.GetString(4),
                reader.IsDBNull(5) ? null : reader.GetString(5),
                reader.IsDBNull(6) ? null : reader.GetString(6)));
        }

        return list.Count > 0 ? list : GetFallbackDefinitions();
    }

    public async Task<GameDefinitionDto?> GetGameDefinitionByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var all = await GetGameDefinitionsAsync(cancellationToken);
        return all.FirstOrDefault(x => string.Equals(x.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IReadOnlyList<GameSessionDto>> GetGameSessionsAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
            return Array.Empty<GameSessionDto>();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT id, game_definition_id, title, status, created_at
            FROM game_sessions
            ORDER BY created_at DESC
            """;

        await using var cmd = new NpgsqlCommand(sql, connection);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<GameSessionDto>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new GameSessionDto(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetDateTime(4)));
        }

        return list;
    }

    public async Task<IReadOnlyList<GameLobbyDto>> GetLobbiesAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
            return Array.Empty<GameLobbyDto>();

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = """
            SELECT id, game_definition_id, name, is_open, created_at
            FROM game_lobbies
            ORDER BY created_at DESC
            """;

        await using var cmd = new NpgsqlCommand(sql, connection);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        var list = new List<GameLobbyDto>();
        while (await reader.ReadAsync(cancellationToken))
        {
            list.Add(new GameLobbyDto(
                reader.GetInt32(0),
                reader.GetInt32(1),
                reader.GetString(2),
                reader.GetBoolean(3),
                reader.GetDateTime(4)));
        }

        return list;
    }

    private static IReadOnlyList<GameDefinitionDto> GetFallbackDefinitions() =>
    [
        new(1, "kroko", "Kroko", "Draw and go go", "assets/images/brushes-paint-and-palletes.png", "Kroko", "kroko"),
        new(2, "geo-guesser", "GeoGuesser", "Find a belarussian place", "assets/images/piece-of-papper-map.png", "GeoGuesser", "geo-guesser"),
        new(3, "phaser-test", "PhaserTest", "Phaser test game", "assets/images/piece-of-papper-map.png", "PhaserTest", "phaser-test"),
    ];
}
