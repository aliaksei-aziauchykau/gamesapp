using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Exceptions;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class PatchEntityCommandTests
{
    private readonly Mock<ICrudService<TestEntity, int>> _crudServiceMock;
    private readonly PatchEntityCommandHandler<TestEntity, int> _handler;

    public PatchEntityCommandTests()
    {
        _crudServiceMock = new Mock<ICrudService<TestEntity, int>>();
        _handler = new PatchEntityCommandHandler<TestEntity, int>(_crudServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPatchedEntity()
    {
        // Arrange
        var entityId = 1;
        var patcherMock = new Mock<IDelta<TestEntity>>();
        var patchedEntity = new TestEntity { Id = entityId };
        _crudServiceMock.Setup(x => x.PatchAsync(entityId, patcherMock.Object, It.IsAny<CancellationToken>()))
            .ReturnsAsync(patchedEntity);

        var command = new PatchEntityCommand<TestEntity, int>(entityId, patcherMock.Object);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(patchedEntity, result);
        _crudServiceMock.Verify(x => x.PatchAsync(entityId, patcherMock.Object, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenEntityNotFound()
    {
        // Arrange
        var entityId = 1;
        var patcherMock = new Mock<IDelta<TestEntity>>();
        _crudServiceMock.Setup(x => x.PatchAsync(entityId, patcherMock.Object, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException());

        var command = new PatchEntityCommand<TestEntity, int>(entityId, patcherMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }
}
