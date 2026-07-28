using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using BGP.Auth.Application.Modules.AuthApiManagement;
using BGP.Auth.Domain.Entities;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Services;
using MediatR;

namespace BGP.Auth.Application.Modules.AuthUsers;

public class AuthUsersCrudService : ICrudService<AuthUser, Guid>
{
    private readonly AuthManagementApiConfig authManagementApiConfig;
    private readonly IMediator mediator;

    public AuthUsersCrudService(AuthManagementApiConfig authManagementApiConfig, IMediator mediator)
    {
        this.authManagementApiConfig = authManagementApiConfig;
        this.mediator = mediator;
    }

    public Task<AuthUser> AddAsync(AuthUser entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuthUser> AddOrUpdateAsync(AuthUser entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuthUser> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<AuthUser>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var managementApiClient = await this.GetManagementApiClientAsync();
        var users = await managementApiClient.Users.GetAllAsync(new GetUsersRequest());

        throw new NotImplementedException();
    }

    public Task<IQueryable<AuthUser>> GetAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuthUser> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<AuthUser?>> GetByIdQueryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuthUser> PatchAsync(Guid id, IDelta<AuthUser> patcher, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AuthUser> UpdateAsync(AuthUser entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    private async Task<ManagementApiClient> GetManagementApiClientAsync()
    {
        // var domain = authManagementApiConfig.Domain;
        var audience = authManagementApiConfig.Audience;
        var token = await mediator.Send(new GetApiManagementAccessTokenCommand());

        var managementApiClient = new ManagementApiClient(token, new Uri(audience));

        return managementApiClient;
    }
}
