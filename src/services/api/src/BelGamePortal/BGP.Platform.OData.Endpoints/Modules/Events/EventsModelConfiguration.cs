
using Asp.Versioning;
using Asp.Versioning.OData;
using BGP.Platform.Domain.Entities;
using Microsoft.OData.ModelBuilder;

namespace BGP.Platform.OData.Endpoints.Modules.Events;

public class EventsModelConfiguration : IModelConfiguration
{
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string routePrefix)
    {
        var set = builder.EntitySet<Event>("Events");
        var type = set.EntityType;

        type.HasKey(o => o.EventId);
        type.Select().Expand().Filter().Count();
    }
}
