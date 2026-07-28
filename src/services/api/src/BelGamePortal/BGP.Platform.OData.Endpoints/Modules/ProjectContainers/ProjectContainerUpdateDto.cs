using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectContainers;

public class ProjectContainerUpdateDto : IMapTo<ProjectContainer>
{
    public Guid ProjectId { get; set; }

    public Guid ContainerId { get; set; }
}
