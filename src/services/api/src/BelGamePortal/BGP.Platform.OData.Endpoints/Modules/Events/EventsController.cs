using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.Events;

public class EventsController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<Event, Guid, EventCreateDto, EventUpdateDto, EventPatchDto>(microservice)
{
}
