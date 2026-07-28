using BGP.Platform.Domain.Entities;
using BGP.Platform.Microservice.Interfaces;
using BGP.Shared.Modules.OData.Crud.Api;

namespace BGP.Platform.OData.Endpoints.Modules.Posts;

public class PostsController(IPlatformMicroservice microservice) : CrudODataControllerExtendedBase<Post, Guid, PostCreateDto, PostUpdateDto, PostPatchDto>(microservice)
{
}
