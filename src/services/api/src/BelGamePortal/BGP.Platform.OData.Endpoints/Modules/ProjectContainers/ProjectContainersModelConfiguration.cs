
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectContainers;

public class ProjectContainersModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var set = builder.EntitySet<ProjectContainer>("ProjectContainers");
        var type = set.EntityType;

        type.HasKey(o => o.ProjectContainerId);
        type.Select().Expand().Filter().Count();
    }
}
