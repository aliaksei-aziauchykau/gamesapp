using System.Text;
using Auth0.ManagementApi;
using Newtonsoft.Json;

namespace BGP.Auth.Application.Modules.AuthApiManagement;

public class ManagementApiClientProvider(IManagementApiTokenProvider managementApiTokenProvider) : IManagementApiClientProvider
{
    public Task<ManagementApiClient> CreateManagementApiClientAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
