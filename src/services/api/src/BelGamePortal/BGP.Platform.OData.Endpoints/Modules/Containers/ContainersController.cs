using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.Containers;

public class ContainersController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<Container, Guid, ContainerCreateDto, ContainerUpdateDto, ContainerPatchDto>(microservice)
{
}
