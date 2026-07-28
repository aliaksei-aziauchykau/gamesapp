using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.Workspaces;

public class WorkspacePatchDto : IMapToDelta<Workspace>
{
    public string? Name { get; set; }
}
