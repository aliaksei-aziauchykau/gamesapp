using System.Text;
using Newtonsoft.Json;

namespace BGP.Auth.Application.Modules.AuthApiManagement;

public class ManagementApiTokenProvider(AuthManagementApiConfig authManagementApiConfig) : IManagementApiTokenProvider
{
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        using var client = new HttpClient();

        var clientId = authManagementApiConfig.ClientId;
        var clientSecret = authManagementApiConfig.ClientSecret;
        var domain = authManagementApiConfig.Domain;
        var audience = authManagementApiConfig.Audience;
        var grantType = "client_credentials";

        var requestBody = new
        {
            client_id = clientId,
            client_secret = clientSecret,
            audience,
            grant_type = grantType
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(authManagementApiConfig.TokenUrl, requestContent);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content) ?? throw new Exception("Token response is null");

        return tokenResponse.AccessToken;
    }
}
