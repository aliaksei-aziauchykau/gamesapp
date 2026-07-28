using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;
using Xunit;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class GetEntitiesCommandTests
{
    private readonly Mock<ICrudService<TestEntity, int>> _crudServiceMock;
    private readonly GetEntitiesCommandHandler<TestEntity, int> _handler;

    public GetEntitiesCommandTests()
    {
        _crudServiceMock = new Mock<ICrudService<TestEntity, int>>();
        _handler = new GetEntitiesCommandHandler<TestEntity, int>(_crudServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnQueryableEntities()
    {
        // Arrange
        var entities = new[] { new TestEntity { Id = 1 }, new TestEntity { Id = 2 } }.AsQueryable();
        _crudServiceMock.Setup(service => service.GetAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        var command = new GetEntitiesCommand<TestEntity, int>();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(entities, result);
        _crudServiceMock.Verify(service => service.GetAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }
}
