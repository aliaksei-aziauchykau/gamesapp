using BGP.Shared.Modules.CachePipeline;
using MediatR;

namespace BGP.Auth.Application.Modules.AuthApiManagement;

[CachePipeline]
public class GetApiManagementAccessTokenCommand : IRequest<string>
{
}

public class GetApiManagementAccessTokenCommandHandler : IRequestHandler<GetApiManagementAccessTokenCommand, string>
{
    public async Task<string> Handle(GetApiManagementAccessTokenCommand request, CancellationToken cancellationToken)
    {
        return "token";
    }
}
