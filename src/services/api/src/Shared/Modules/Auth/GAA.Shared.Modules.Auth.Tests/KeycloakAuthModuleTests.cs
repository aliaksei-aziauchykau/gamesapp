using GAA.Shared.Modules.Auth.Implementation;
using Xunit;

namespace GAA.Shared.Modules.Auth.Tests;

public class KeycloakAuthModuleTests
{
    [Fact]
    public void Create_returns_instance()
    {
        var module = KeycloakAuthModule.Create();
        Assert.NotNull(module);
    }
}
