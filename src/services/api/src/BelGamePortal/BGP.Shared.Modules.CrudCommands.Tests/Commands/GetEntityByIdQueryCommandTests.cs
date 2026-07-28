using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.Services;
using Moq;
using Xunit;

namespace BGP.Shared.Modules.CrudCommands.Tests.Commands;

public class GetEntityByIdQueryCommandTests
{
    [Fact]
    public async Task Handle_ReturnsEntity_WhenEntityExists()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<TestEntity, int>>();
        var testEntity = new TestEntity { Id = 1 };
        var queryableEntity = new[] { testEntity }.AsQueryable();
        mockCrudService.Setup(service => service.GetByIdQueryAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(queryableEntity);

        var handler = new GetEntityByIdQueryCommandHandler<TestEntity, int>(mockCrudService.Object);
        var command = new GetEntityByIdQueryCommand<TestEntity, int>(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(testEntity, result.First());
    }

    [Fact]
    public async Task Handle_ReturnsEmpty_WhenEntityDoesNotExist()
    {
        // Arrange
        var mockCrudService = new Mock<ICrudService<TestEntity, int>>();
        var queryableEntity = Enumerable.Empty<TestEntity>().AsQueryable();
        mockCrudService.Setup(service => service.GetByIdQueryAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(queryableEntity);

        var handler = new GetEntityByIdQueryCommandHandler<TestEntity, int>(mockCrudService.Object);
        var command = new GetEntityByIdQueryCommand<TestEntity, int>(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    public class TestEntity : IHasKey<int>
    {
        public int Id { get; set; }
    }
}
