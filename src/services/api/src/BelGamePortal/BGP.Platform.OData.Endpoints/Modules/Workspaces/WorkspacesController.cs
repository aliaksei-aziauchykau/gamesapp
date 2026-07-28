using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.Workspaces;

public class WorkspacesController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<Workspace, Guid, WorkspaceCreateDto, WorkspaceUpdateDto, WorkspacePatchDto>(microservice)
{
}
