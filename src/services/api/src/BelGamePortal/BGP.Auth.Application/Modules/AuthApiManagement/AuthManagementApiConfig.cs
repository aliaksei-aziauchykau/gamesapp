namespace BGP.Auth.Application.Modules.AuthApiManagement;

public class AuthManagementApiConfig
{
    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required string Domain { get; init; }

    public required string Audience { get; init; }

    public required string TokenUrl { get; init; }
}
