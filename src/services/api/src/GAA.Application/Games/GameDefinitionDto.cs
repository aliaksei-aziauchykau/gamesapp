namespace GAA.Application.Games;

public sealed record GameDefinitionDto(
    int Id,
    string Code,
    string Name,
    string? Description,
    string? ImageUrl,
    string? ImageAlt,
    string? GamePageAppUrl);
