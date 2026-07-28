

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreatePageCommand : IRequest
{
    public required string ModuleName { get; init; }


}

// Handler class
public class UiCreatePageCommandHandler : IRequestHandler<UiCreatePageCommand>
{
    public async Task Handle(UiCreatePageCommand command, CancellationToken cancellationToken)
    {
        var taskRunner = UiPageModuleGenerator
            .Create()
            .WithModuleName(command.ModuleName)
            .WithRootFolderPath()
            .WithPage()
            .WithContent()
            .GetTaskRunner();

        await taskRunner.RunAllAsync();
    }
}
