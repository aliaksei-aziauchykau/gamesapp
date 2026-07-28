namespace BGP.Shared.Modules.Email;

public class EmailConfiguration : IEmailConfiguration
{
    public required string UserName { get; set; }

    public required string Password { get; set; }
}
