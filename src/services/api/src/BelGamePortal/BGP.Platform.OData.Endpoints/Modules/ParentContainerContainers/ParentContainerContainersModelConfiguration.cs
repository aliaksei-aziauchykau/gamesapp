
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.ParentContainerChildContainers;

public class ParentContainerContainersModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<ParentContainerChildContainer>("ParentContainerChildContainers");
        var type = set.EntityType;

        type.HasKey(o => o.ParentContainerChildContainerId);
        type.Select().Expand().Filter().Count();
    }
}
