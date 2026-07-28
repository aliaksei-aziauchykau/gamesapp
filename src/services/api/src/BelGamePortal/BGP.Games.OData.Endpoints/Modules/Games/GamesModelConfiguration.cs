
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities;
using BGP.Shared.Libs.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.Games;

public class GamesConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Game>("Games");
        var type = set.EntityType;

        type.HasKey(o => (o.Id));
        type.Select().Expand().Filter().Count();
    }
}
