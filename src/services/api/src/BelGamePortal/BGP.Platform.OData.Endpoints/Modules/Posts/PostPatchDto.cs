using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.Posts;

public class PostPatchDto : IMapToDelta<Post>
{
    public Guid? ContainerId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }
}
