using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectContainers;

public class ProjectContainersController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<ProjectContainer, int, ProjectContainerCreateDto, ProjectContainerUpdateDto, ProjectContainerPatchDto>(microservice)
{
}
