using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class AddOrUpdateEntityCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnEntity_WhenEntityIsAddedOrUpdated()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<Entity, int>>();
        var testEntity = new Entity { Id = 1, Name = "Test" };
        mockCrudService.Setup(service => service.AddOrUpdateAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(testEntity);

        var handler = new AddOrUpdateEntityCommandHandler<Entity, int>(mockCrudService.Object);
        var command = new AddOrUpdateEntityCommand<Entity, int>(testEntity);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(testEntity, result);
        mockCrudService.Verify(service => service.AddOrUpdateAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    public class Entity : IHasKey<int>
    {
        public int Id { get; init; }
        public required string Name { get; init; }
    }
}
