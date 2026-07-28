using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class DeleteEntityCommandTests
{
    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }

    [Fact]
    public async Task Handle_ShouldDeleteEntity()
    {
        // Arrange
        var entityId = 1;
        var entity = new TestEntity { Id = entityId };
        var mockCrudService = new Mock<ICrudService<TestEntity, int>>();
        mockCrudService.Setup(service => service.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var handler = new DeleteEntityCommandHandler<TestEntity, int>(mockCrudService.Object);
        var command = new DeleteEntityCommand<TestEntity, int>(entityId);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(entityId, result.Id);
        mockCrudService.Verify(service => service.DeleteAsync(It.Is<int>(e => e == entityId), It.IsAny<CancellationToken>()), Times.Once);
    }
}
