using MediatR;

namespace BGP.Shared.Modules.GameLogic.Kroko.GameCommands.SaveGameSettings;

public class KrokoSaveGameSettingsCommand : IRequest
{
    public IKrokoGameSettings GameSettings { get; set; }

    public KrokoSaveGameSettingsCommand(IKrokoGameSettings gameSettings)
    {
        this.GameSettings = gameSettings;
    }
}

public class KrokoSaveGameSettingsCommandHandler : IRequestHandler<KrokoSaveGameSettingsCommand>
{


    public KrokoSaveGameSettingsCommandHandler()
    {
    }

    public async Task Handle(KrokoSaveGameSettingsCommand command, CancellationToken cancellationToken)
    {

    }
}
