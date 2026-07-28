
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.Workspaces;

public class WorkspacesModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Workspace>("Workspaces");
        var type = set.EntityType;

        type.HasKey(o => o.WorkspaceId);
        type.Select().Expand().Filter().Count();
    }
}
