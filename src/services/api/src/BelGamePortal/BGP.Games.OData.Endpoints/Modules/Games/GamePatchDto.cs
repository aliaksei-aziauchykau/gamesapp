using System.Text.Json.Serialization;
using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;


namespace BGP.Games.OData.Endpoints.Modules.Games;

public class GamePatchDto : IMapToDelta<Game>
{
    public string? Name { get; set; }

    public required string Code { get; set; }

    public int? GameDefinitionId { get; set; }
}
