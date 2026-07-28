using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Games.Domain.Entities.Games.Kroko;
using Microsoft.OData.ModelBuilder;

namespace BGP.Games.OData.Endpoints.Modules.Kroko.Word;

public class KrokoWordModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        //builder.EntityType<Player>().ha;

        var entitySet = builder.EntitySet<KrokoWord>("KrokoWords");
        var entityType = entitySet.EntityType;

        // entityType.HasKey(o => o.Id);
        entityType.Select().Expand().Filter();

        //entityType.Function("MostExpensive").Returns<IList<KrokoWord>>();
        // entityType.Collection.Function("GetRandomWords").ReturnsCollectionFromEntitySet<KrokoWord>("KrokoWords");
        //.Expand(SelectExpandType.Allowed);

        //builder.EntityType<Game>().HasKey(o => o.Id);
        //builder.EntityType<Player>().HasKey(o => o.Id);
    }
}
