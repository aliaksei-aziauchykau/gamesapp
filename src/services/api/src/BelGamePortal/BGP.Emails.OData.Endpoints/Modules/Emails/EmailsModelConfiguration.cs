
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Emails.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Emails.OData.Endpoints.Modules.Emails;

public class EmailsConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Email>("Emails");
        var type = set.EntityType;

        // For some reason, the following line is not working
        // type.HasKey(o => o.Id);
        type.Select().Expand().Filter().Count();
    }
}
