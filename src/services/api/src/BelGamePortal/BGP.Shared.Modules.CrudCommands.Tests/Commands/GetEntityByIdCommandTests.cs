using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class GetEntityByIdCommandTests
{
    public class Entity : IHasKey<int>
    {
        public int Id { get; set; }
    }

    [Fact]
    public async Task Handle_ReturnsEntity_WhenEntityExists()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<Entity, int>>();
        var testEntity = new Entity { Id = 1 };
        mockCrudService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(testEntity);

        var handler = new GetEntityByIdCommandHandler<Entity, int>(mockCrudService.Object);
        var command = new GetEntityByIdCommand<Entity, int>(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(testEntity, result);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<Entity, int>>();
        mockCrudService.Setup(service => service.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Entity)null);

        var handler = new GetEntityByIdCommandHandler<Entity, int>(mockCrudService.Object);
        var command = new GetEntityByIdCommand<Entity, int>(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}
