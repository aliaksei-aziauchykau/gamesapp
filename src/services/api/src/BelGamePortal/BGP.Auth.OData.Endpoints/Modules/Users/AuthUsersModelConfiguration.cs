
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Auth.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Emails.OData.Api.Modules.Emails;

public class AuthUsersModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        var set = builder.EntitySet<AuthUser>("AuthUsers");
        var type = set.EntityType;

        type.HasKey(o => o.Id);
        type.Select().Expand().Filter().Count();
    }
}
