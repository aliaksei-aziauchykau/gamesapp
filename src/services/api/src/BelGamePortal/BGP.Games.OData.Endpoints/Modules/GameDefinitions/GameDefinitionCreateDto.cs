using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Games.OData.Endpoints.Modules.GameDefinitions;

public class GameDefinitionCreateDto : IMapTo<GameDefinition>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageAlt { get; set; }

    public string? GamePageAppUrl { get; set; }

}
