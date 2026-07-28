

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateApiCommand : IRequest
{
    public required string ModuleName { get; init; }

    public required string EntityName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateApiCommandHandler : IRequestHandler<UiCreateApiCommand>
{
    public async Task Handle(UiCreateApiCommand command, CancellationToken cancellationToken)
    {
        var builder = UIApiModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithEntityName(command.EntityName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder.WithApi();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
