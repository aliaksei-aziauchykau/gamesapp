using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.Projects;

public class ProjectCreateDto : IMapTo<Project>
{
    public string Name { get; set; }

    public int ProjectVisibilityId { get; set; }
}
