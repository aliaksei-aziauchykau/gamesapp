using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.Projects;

public class ProjectsController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<Project, Guid, ProjectCreateDto, ProjectUpdateDto, ProjectPatchDto>(microservice)
{
}
