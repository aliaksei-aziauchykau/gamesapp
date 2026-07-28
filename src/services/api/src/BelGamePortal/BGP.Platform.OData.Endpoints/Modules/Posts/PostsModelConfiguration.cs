
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.Posts;

public class PostsModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Post>("Posts");
        var type = set.EntityType;

        type.HasKey(o => o.PostId);
        type.Select().Expand().Filter().Count();
    }
}
