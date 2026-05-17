namespace GAA.Shared.Modules.Auth.Interfaces;

public class KeycloakAuthConfiguration
{
    /// <summary>Issuer URL, e.g. https://keycloak/realms/myrealm </summary>
    public required string Authority { get; init; }

    /// <summary>Optional API audience (Keycloak client id for the API).</summary>
    public string? Audience { get; init; }

    public bool RequireHttpsMetadata { get; init; } = true;
}
