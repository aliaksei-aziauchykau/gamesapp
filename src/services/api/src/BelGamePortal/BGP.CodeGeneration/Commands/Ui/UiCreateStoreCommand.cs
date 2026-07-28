

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateStoreCommand : IRequest
{
    public required string ModuleName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateStoreCommandHandler : IRequestHandler<UiCreateStoreCommand>
{
    public async Task Handle(UiCreateStoreCommand command, CancellationToken cancellationToken)
    {
        var builder = UiStoreModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder.WithStore();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
