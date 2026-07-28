
using BGP.Platform.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Repositories;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Platform.Application.Modules.Posts;

public class PostsCrudService(IRepository<Post, Guid> repository, IMediator mediator) : CrudService<Post, Guid>(repository)
{
    public override Task<Post> AddAsync(Post entity, CancellationToken cancellationToken = default)
    {
        var created = base.AddAsync(entity, cancellationToken);

        mediator.Publish(new PostCreatedEvent());

        return created;
    }

}
