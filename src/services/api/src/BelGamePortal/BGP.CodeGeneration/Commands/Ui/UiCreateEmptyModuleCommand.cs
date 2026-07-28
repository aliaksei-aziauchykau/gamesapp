

using System.Threading;
using System.Threading.Tasks;
using System.Web.Services.Description;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateEmptyModuleCommand : IRequest
{
    public required string ModuleName { get; init; }

    public string? BaseFolderPath { get; init; }
}

public class UiCreateEmptyModuleCommandHandler : IRequestHandler<UiCreateEmptyModuleCommand>
{
    public async Task Handle(UiCreateEmptyModuleCommand command, CancellationToken cancellationToken)
    {
        var builder = UIEmptyModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithRootFolderPath();

        if (!string.IsNullOrEmpty(command.BaseFolderPath))
        {
            builder.WithBaseFolderPath(command.BaseFolderPath);
        }

        builder.WithEmptyModule();

        var taskRunner = builder.GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
