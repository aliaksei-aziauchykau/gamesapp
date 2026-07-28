using BGP.Platform.Domain.Entities;
using BGP.Shared.Libs.Mappings.Mappings;

namespace BGP.Platform.OData.Endpoints.Modules.Events;

public class EventUpdateDto : IMapTo<Event>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid ContainerId { get; set; }
}
