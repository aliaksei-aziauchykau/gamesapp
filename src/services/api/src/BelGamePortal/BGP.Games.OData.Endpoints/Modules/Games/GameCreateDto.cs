using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Games.OData.Endpoints.Modules.Games;

public class GameCreateDto : IMapTo<Game>
{
    public string? Name { get; set; }

    public int? GameDefinitionId { get; set; }
}
