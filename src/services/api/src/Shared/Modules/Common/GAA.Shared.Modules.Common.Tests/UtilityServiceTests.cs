using GAA.Shared.Modules.Common.Implementation.Services;
using Xunit;

namespace GAA.Shared.Modules.Common.Tests;

public class UtilityServiceTests
{
    [Fact]
    public void ApplicationName_is_set()
    {
        var sut = new UtilityService();
        Assert.Equal("GamesApp API", sut.ApplicationName);
    }
}
