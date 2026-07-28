

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateFeatureCommand : IRequest
{
    public required string ModuleName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateFeatureCommandHandler : IRequestHandler<UiCreateFeatureCommand>
{
    public async Task Handle(UiCreateFeatureCommand command, CancellationToken cancellationToken)
    {
        var builder = UiFeatureModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder.WithFeature();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
