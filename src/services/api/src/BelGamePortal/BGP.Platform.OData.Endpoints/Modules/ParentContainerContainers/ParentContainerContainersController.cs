using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.ParentContainerChildContainers;

public class ParentContainerChildContainersController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<ParentContainerChildContainer, int, ParentContainerChildContainerCreateDto, ParentContainerChildContainerUpdateDto, ParentContainerChildContainerPatchDto>(microservice)
{
}
