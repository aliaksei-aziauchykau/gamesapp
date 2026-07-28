using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.ProjectVisibilitys;

public class ProjectVisibilitiesController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<ProjectVisibility, int, ProjectVisibilityCreateDto, ProjectVisibilityUpdateDto, ProjectVisibilityPatchDto>(microservice)
{
}
