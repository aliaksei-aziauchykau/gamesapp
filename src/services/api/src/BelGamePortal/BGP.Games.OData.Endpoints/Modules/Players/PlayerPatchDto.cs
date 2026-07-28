using System.Text.Json.Serialization;
using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Games.OData.Endpoints.Modules.Players;

public class PlayerPatchDto : IMapToDelta<Player>
{
    public string? Name { get; set; }

    public Guid? GameId { get; set; }

}
