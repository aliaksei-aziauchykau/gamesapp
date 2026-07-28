
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.Players;

public class PlayersConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Player>("Players");
        var type = set.EntityType;

        type.HasKey(o => o.Id);
        type.Select().Expand().Filter().Count();
    }
}
