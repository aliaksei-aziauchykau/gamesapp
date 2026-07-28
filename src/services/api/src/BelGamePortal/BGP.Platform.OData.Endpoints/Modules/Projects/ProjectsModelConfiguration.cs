
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.Projects;

public class ProjectsModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var set = builder.EntitySet<Project>("Projects");
        var type = set.EntityType;

        type.HasKey(o => o.ProjectId);
        type.Select().Expand().Filter().Count();
    }
}
