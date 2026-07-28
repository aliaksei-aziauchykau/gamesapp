
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.Containers;

public class ContainersModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Container>("Containers");
        var type = set.EntityType;

        type.HasKey(o => o.ContainerId);
        type.Select().Expand().Filter().Count();
    }
}
