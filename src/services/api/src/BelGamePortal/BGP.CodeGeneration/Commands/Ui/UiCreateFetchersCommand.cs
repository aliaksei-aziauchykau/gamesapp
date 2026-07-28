

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateFetchersCommand : IRequest
{
    public required string ModuleName { get; init; }

    public required string EntityName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateFetchersCommandHandler : IRequestHandler<UiCreateFetchersCommand>
{
    public async Task Handle(UiCreateFetchersCommand command, CancellationToken cancellationToken)
    {
        var builder = UIFetchersModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithEntityName(command.EntityName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder
            .WithItemFetcher()
            .WithListFetcher();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
