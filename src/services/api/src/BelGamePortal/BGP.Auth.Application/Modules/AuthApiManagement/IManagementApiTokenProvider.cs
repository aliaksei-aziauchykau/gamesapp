namespace BGP.Auth.Application.Modules.AuthApiManagement;

public interface IManagementApiTokenProvider
{
    Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
}
