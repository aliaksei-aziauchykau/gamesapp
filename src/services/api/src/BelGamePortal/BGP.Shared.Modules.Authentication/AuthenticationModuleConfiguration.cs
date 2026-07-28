namespace BGP.Shared.Modules.Authentication;

public class AuthenticationModuleConfiguration
{
    public required string Authority { get; init; }

    public required string Audience { get; init; }
}
