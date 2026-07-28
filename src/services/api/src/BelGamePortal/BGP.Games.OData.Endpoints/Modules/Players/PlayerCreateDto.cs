using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Games.OData.Endpoints.Modules.Players;

public class PlayerCreateDto : IMapTo<Player>
{
    public string? Name { get; set; }

    public Guid? GameId { get; set; }

}
