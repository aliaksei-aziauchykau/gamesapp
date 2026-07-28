using System.Text;
using Auth0.ManagementApi;
using Newtonsoft.Json;

namespace BGP.Auth.Application.Modules.AuthApiManagement;

public interface IManagementApiClientProvider
{
    Task<ManagementApiClient> CreateManagementApiClientAsync(CancellationToken cancellationToken = default);
}
