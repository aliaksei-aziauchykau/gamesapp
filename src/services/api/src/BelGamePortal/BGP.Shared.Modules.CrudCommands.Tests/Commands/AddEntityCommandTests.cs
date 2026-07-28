
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class AddEntityCommandTest
{
    [Fact]
    public async Task Handle_ShouldAddEntity()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<Entity, int>>();
        var testEntity = new Entity { Id = 1 };
        mockCrudService.Setup(service => service.AddAsync(It.IsAny<Entity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(testEntity);

        var handler = new AddEntityCommandHandler<Entity, int>(mockCrudService.Object);
        var command = new AddEntityCommand<Entity, int>(testEntity);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(testEntity, result);
        mockCrudService.Verify(service => service.AddAsync(It.Is<Entity>(e => e == testEntity), It.IsAny<CancellationToken>()), Times.Once);
    }

    public class Entity : IHasKey<int>
    {
        public int Id { get; set; }
    }


}
