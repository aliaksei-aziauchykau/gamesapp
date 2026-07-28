using BGP.Platform.Domain.Entities;
using BGP.Platform.OData.Endpoints.Modules.Projects;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.Workspaces;

public class WorkspaceCreateDto : IMapTo<Workspace>
{
    public string Name { get; set; }

    public ICollection<ProjectCreateDto> Projects { get; set; }
}
