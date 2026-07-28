using AutoMapper;
using BGP.Games.Microservice.Interfaces;
using BGP.Shared.Libs.CrudServices.Interfaces.Services;
using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Interfaces;
using BGP.Shared.Modules.EntityApi;
using BGP.Shared.Modules.GameLogic.Kroko.Commands;
using BGP.Shared.Modules.GameLogic.Kroko.Commands.JoinPlayer;
using BGP.Shared.Modules.GameLogic.Kroko.Commands.LeavePlayer;
using BGP.Shared.Modules.GameLogic.Kroko.Container;
using BGP.Shared.Modules.GameLogic.Kroko.Context;
using BGP.Shared.Modules.GameLogic.Kroko.GameCommands.GuessWord;
using BGP.Shared.Modules.GameLogic.Kroko.GameStates;
using BGP.Shared.Modules.GameLogic.Kroko.Interfaces;
using BGP.Shared.Modules.GameLogic.Kroko.Logic;
using BGP.Shared.Modules.GameLogic.Kroko.Restorer;
using BGP.Shared.Modules.GameLogic.Kroko.StateRestorer;
using BGP.Shared.Modules.GameLogic.Kroko.States;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Unity;

namespace BGP.Shared.Modules.GameLogic.Kroko.ContainerCreator;

public class KrokoGameContainerCreator : GameContainerCreator, IKrokoGameContainerCreator
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly IList<Type> externalServices = new List<Type>
    {
        typeof(IGameContainerHubContext),
        typeof(IKrokoGameHubContext),
        typeof(ILogger)
    };

    public KrokoGameContainerCreator(
        IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override string Name => GameDefNames.Kroko;

    public override IGameContainer Create(string gameId)
    {
        IUnityContainer container = CreateContainer()
            .AddMediator()
            .AddBasicCommands()
            .UnityContainer;

        container.RegisterInstance<IKrokoGameContext>(new KrokoGameContext(gameId));
        IGameContext gameContext = container.Resolve<IKrokoGameContext>();
        container.RegisterInstance(gameContext);

        container.RegisterInstance<IKrokoGameSettings>(new KrokoGameSettings(gameId));
        IGameSettings gameSettings = container.Resolve<IKrokoGameSettings>();
        container.RegisterInstance(gameSettings);

        container.RegisterType<IGamePerformer, GamePerformer>(TypeLifetime.Singleton);
        container.RegisterType<IGameLogic, KrokoGameLogic>(TypeLifetime.Singleton);
        container.RegisterType<IGameRestorer, KrokoGameRestorer>(TypeLifetime.Singleton);
        container.RegisterType<IGameStateRestorer, KrokoGameStateRestorer>(TypeLifetime.Singleton);

        container.RegisterType(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        container.RegisterType(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
        container.RegisterType(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        container.RegisterType(typeof(IRequestPreProcessor<>), typeof(SimpleLoggingBehavior<>));

        // Validators
        container.RegisterType<IValidator<KrokoGuessWordGameCommand>, KrokoGuessWordGameCommandValidator>();

        // Commands
        container.RegisterType<IRequestHandler<KrokoGameCommand>, KrokoGameCommandHandler>();
        // container.RegisterType<IRequestHandler<KrokoGameContextSaveCommand>, KrokoGameContextSaveCommandHandler>();
        container.RegisterType<IRequestHandler<KrokoJoinPlayerGameCommand>, KrokoJoinPlayerGameCommandHandler>();
        container.RegisterType<IRequestHandler<KrokoLeavePlayerGameCommand>, KrokoLeavePlayerGameCommandHandler>();
        container.RegisterType<IRequestHandler<KrokoGuessWordGameCommand>, KrokoGuessWordGameCommandHandler>();
        container.RegisterType<IRequestHandler<KrokoDrawWordGameCommand>, KrokoDrawWordGameCommandHandler>();
        container.RegisterType<IRequestHandler<KrokoStoreSelectedWordGameCommand>, KrokoStoreSelectedWordGameCommandHandler>();

        // States
        container.RegisterType<IKrokoInitialGameState, KrokoInitialGameState>();
        container.RegisterType<IKrokoChangeDrawerGameState, KrokoChangeDrawerGameState>();
        container.RegisterType<IKrokoStoreSelectedWordGameState, KrokoStoreSelectedWordGameState>();
        container.RegisterType<IKrokoPlayerSelectWordGameState, KrokoPlayerSelectWordGameState>();
        container.RegisterType<IKrokoDrawGuessWordGameState, KrokoDrawGuessWordGameState>();
        container.RegisterType<IKrokoWordWasGuessedGameState, KrokoWordWasGuessedGameState>();
        container.RegisterType<IKrokoWordWasNotGuessedGameState, KrokoWordWasNotGuessedGameState>();
        container.RegisterType<IKrokoLoadNewRoundGuessedGameState, KrokoLoadNewRoundGameState>();
        container.RegisterType<IKrokoGameCompletedGameState, KrokoGameCompletedGameState>();

        using var scope = _serviceScopeFactory.CreateScope();

        foreach (var externalService in externalServices)
        {
            container.RegisterInstance(externalService, scope.ServiceProvider.GetRequiredService(externalService));
        }


        // var krokoWordEntityApiClient = scope.ServiceProvider.GetRequiredService<IEntityApiClient<KrokoWord, int>>();
        // var krokoGameSettingsEntityApiClient = scope.ServiceProvider.GetRequiredService<IEntityApiClient<KrokoGameSettings, string>>();
        var microservice = scope.ServiceProvider.GetRequiredService<IGamesMicroservice>();
        container.RegisterInstance(microservice.Scope.ServiceProvider.GetRequiredService<IKrokoGameDataService>());

        // var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        // var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

        var gameContainerHubContext = scope.ServiceProvider.GetRequiredService<IGameContainerHubContext>();

        var gameLogic = container.Resolve<IGameLogic>();
        gameLogic.GameStateWasChanged += async (s, gameStateName) => await gameContainerHubContext.SendToGameGroupAsync(gameId, "GameStateChanged", gameStateName);

        IGameContainer gameContainer = new KrokoGameContainer(container);
        return gameContainer;
    }
}
