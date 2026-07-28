
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectVisibilities;

public class ProjectVisibilitiesModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var set = builder.EntitySet<ProjectVisibility>("ProjectVisibilities");
        var type = set.EntityType;

        type.HasKey(o => o.ProjectVisibilityId);
        type.Select().Expand().Filter().Count();
    }
}
