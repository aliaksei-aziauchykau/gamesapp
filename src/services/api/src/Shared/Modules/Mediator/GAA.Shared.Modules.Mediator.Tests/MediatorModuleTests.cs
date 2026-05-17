using GAA.Shared.Modules.Mediator.Implementation;
using Xunit;

namespace GAA.Shared.Modules.Mediator.Tests;

public class MediatorModuleTests
{
    [Fact]
    public void Create_returns_instance()
    {
        var module = MediatorModule.Create();
        Assert.NotNull(module);
    }
}
