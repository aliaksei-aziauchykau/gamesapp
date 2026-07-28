using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities.Games.Kroko;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.Kroko.WordLanguage;
public class KrokoWordLanguageModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        //builder.EntityType<Player>().ha;

        var entitySet = builder.EntitySet<KrokoWordLanguage>("KrokoWordLanguages");
        var entityType = entitySet.EntityType;

        // entityType.HasKey(o => o.Id);
        entityType.Select().Expand().Filter();
        //.Expand(SelectExpandType.Allowed);

        //builder.EntityType<Game>().HasKey(o => o.Id);
        //builder.EntityType<Player>().HasKey(o => o.Id);
    }
}
