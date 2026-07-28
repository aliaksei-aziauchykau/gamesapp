using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectVisibilitys;

public class ProjectVisibilityUpdateDto : IMapTo<ProjectVisibility>
{
    public string Name { get; set; }

    public string Description { get; set; }
}
