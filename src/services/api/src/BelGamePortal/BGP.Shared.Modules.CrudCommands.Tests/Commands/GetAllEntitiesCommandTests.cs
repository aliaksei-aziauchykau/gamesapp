using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class GetAllEntitiesCommandTests
{
    private readonly Mock<ICrudService<TestEntity, int>> _crudServiceMock;
    private readonly GetAllEntitiesCommandHandler<TestEntity, int> _handler;

    public GetAllEntitiesCommandTests()
    {
        _crudServiceMock = new Mock<ICrudService<TestEntity, int>>();
        _handler = new GetAllEntitiesCommandHandler<TestEntity, int>(_crudServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllEntities()
    {
        // Arrange
        var entities = new List<TestEntity> { new TestEntity { Id = 1 }, new TestEntity { Id = 2 } };
        _crudServiceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        var command = new GetAllEntitiesCommand<TestEntity, int>();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(entities, result);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEntitiesExist()
    {
        // Arrange
        var entities = new List<TestEntity>();
        _crudServiceMock.Setup(service => service.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);
        var command = new GetAllEntitiesCommand<TestEntity, int>();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }
}




















































