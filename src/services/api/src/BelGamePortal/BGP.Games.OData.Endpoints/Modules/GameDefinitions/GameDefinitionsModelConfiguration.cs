
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.GameDefinitions;

public class GameDefinitionsConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<GameDefinition>("GameDefinitions");
        var type = set.EntityType;

        type.HasKey(o => o.Id);
        type.Select().Expand().Filter().Count();
    }
}
