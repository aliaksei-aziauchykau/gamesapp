using System.Threading;
using System.Threading.Tasks;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;
using Xunit;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class UpdateEntityCommandTests
{
    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }

    [Fact]
    public async Task Handle_ShouldReturnUpdatedEntity()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<TestEntity, int>>();
        var testEntity = new TestEntity { Id = 1 };
        mockCrudService.Setup(service => service.UpdateAsync(It.IsAny<TestEntity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(testEntity);

        var handler = new UpdateEntityCommandHandler<TestEntity, int>(mockCrudService.Object);
        var command = new UpdateEntityCommand<TestEntity, int>(testEntity);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(testEntity, result);
        mockCrudService.Verify(service => service.UpdateAsync(It.IsAny<TestEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallUpdateAsyncWithCorrectEntity()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<TestEntity, int>>();
        var testEntity = new TestEntity { Id = 1 };
        mockCrudService.Setup(service => service.UpdateAsync(It.IsAny<TestEntity>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(testEntity);

        var handler = new UpdateEntityCommandHandler<TestEntity, int>(mockCrudService.Object);
        var command = new UpdateEntityCommand<TestEntity, int>(testEntity);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockCrudService.Verify(service => service.UpdateAsync(testEntity, It.IsAny<CancellationToken>()), Times.Once);
    }
}
