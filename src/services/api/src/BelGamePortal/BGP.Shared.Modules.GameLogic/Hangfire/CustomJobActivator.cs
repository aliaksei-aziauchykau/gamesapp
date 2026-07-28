using BGP.Shared.Libs.GameLogic;
using BGP.Shared.Libs.GameLogic.Attributes;
using Hangfire;

namespace BGP.Shared.Modules.GameLogic;

public class CustomJobActivator : JobActivator
{
    private readonly IGameContainerFactory _gameContainerFactory;

    public CustomJobActivator(IGameContainerFactory gameContainerFactory)
    {
        _gameContainerFactory = gameContainerFactory;
    }

    public override object ActivateJob(Type jobType)
    {
        return base.ActivateJob(jobType);
    }

    public override JobActivatorScope BeginScope(JobActivatorContext context)
    {
        if (context.BackgroundJob.Job.Method.GetCustomAttributes(typeof(GameContainerJobAttribute), false).Length > 0)
        {
            var gameId = (string)context.BackgroundJob.Job.Args.First();

            return new GameContainerJobActivatorScope(_gameContainerFactory, gameId);
        }

        return base.BeginScope(context);
    }
}
