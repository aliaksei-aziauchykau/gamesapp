using GAA.Shared.Modules.Common.Interfaces;
using GAA.Shared.Modules.Mediator.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GAA.Shared.Modules.Mediator.Implementation;

public class MediatorModule : DIModule<MediatorModule>, IMediatorModule
{
    public MediatorModule AddMediator(Assembly assembly, MediatorModuleConfiguration? configuration = null)
    {
        var config = configuration ?? new MediatorModuleConfiguration();

        Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            if (config.UsePipelineBehaviors && config.PipelineBehaviors is { Length: > 0 })
            {
                foreach (var behaviorType in config.PipelineBehaviors)
                {
                    cfg.AddBehavior(behaviorType);
                }
            }
        });

        return this;
    }

    public MediatorModule AddMediator(params Assembly[] assemblies)
    {
        Services.AddMediatR(cfg =>
        {
            foreach (var assembly in assemblies)
            {
                cfg.RegisterServicesFromAssembly(assembly);
            }
        });

        return this;
    }

    public void Configure()
    {
    }
}
