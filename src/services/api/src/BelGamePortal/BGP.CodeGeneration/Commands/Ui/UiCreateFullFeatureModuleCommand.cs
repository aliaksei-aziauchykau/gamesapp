

using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Ui;

public record UiCreateFullFeatureCommand : IRequest
{
    public required string ModuleName { get; init; }

    public required string EntityName { get; init; }

    public string? BaseFolderPath { get; init; }

    public bool WithFeatureModule { get; init; }

    public bool WithEntitiesModule { get; init; }

    public bool WithApiModule { get; init; }

    public bool WithFetchersModule { get; init; }
}

public class UiCreateFullFeatureCommandHandler : IRequestHandler<UiCreateFullFeatureCommand>
{
    private readonly IMediator _mediator;

    public UiCreateFullFeatureCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UiCreateFullFeatureCommand command, CancellationToken cancellationToken)
    {

        if (command.WithFeatureModule)
        {
            var featureCommand = new UiCreateFeatureCommand
            {
                ModuleName = command.ModuleName,
                BaseFolderPath = command.BaseFolderPath
            };

            await _mediator.Send(featureCommand, cancellationToken);
        }

        if (command.WithEntitiesModule)
        {
            var entitiesCommand = new UiCreateEntitiesCommand
            {
                EntityName = command.EntityName,
                BaseFolderPath = $"{command.BaseFolderPath}/{command.ModuleName}"
            };

            await _mediator.Send(entitiesCommand, cancellationToken);
        }

        if (command.WithApiModule)
        {
            var apiCommand = new UiCreateApiCommand
            {
                ModuleName = command.ModuleName,
                EntityName = command.EntityName,
                BaseFolderPath = $"{command.BaseFolderPath}/{command.ModuleName}"
            };

            await _mediator.Send(apiCommand, cancellationToken);
        }

        if (command.WithFetchersModule)
        {
            var fetchersCommand = new UiCreateFetchersCommand
            {
                ModuleName = command.ModuleName,
                EntityName = command.EntityName,
                BaseFolderPath = $"{command.BaseFolderPath}/{command.ModuleName}/src/modules"
            };

            await _mediator.Send(fetchersCommand, cancellationToken);
        }
    }
}
