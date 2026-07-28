

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateEntitiesCommand : IRequest
{
    public required string EntityName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateEntitiesCommandHandler : IRequestHandler<UiCreateEntitiesCommand>
{
    public async Task Handle(UiCreateEntitiesCommand command, CancellationToken cancellationToken)
    {
        var builder = UIEntitiesModuleGenerator
            .Create()
            .WithEntityName(command.EntityName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder.WithEntities();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
