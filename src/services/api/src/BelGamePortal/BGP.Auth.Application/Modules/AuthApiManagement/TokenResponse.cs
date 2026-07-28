using Newtonsoft.Json;

namespace BGP.Auth.Application.Modules.AuthApiManagement;

public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }
}
