using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.ParentContainerChildContainers;

public class ParentContainerChildContainerPatchDto : IMapToDelta<ParentContainerChildContainer>
{
    public Guid? ParentContainerId { get; set; }

    public Guid? ChildContainerId { get; set; }
}
