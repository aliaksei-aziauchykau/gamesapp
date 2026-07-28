using System.Reflection;
using BGP.Shared.Libs.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.CachePipeline;

public class CachePipelineModule : DIModule<CachePipelineModule>
{
    public CachePipelineModule AddMemoryCache()
    {
        Services.AddMemoryCache();

        return this;

    }
    public CachePipelineModule AddCachePipelineMediator(Assembly assembly)
    {
        RegisterCachePipelineBehaviors(assembly);

        return this;
    }

    private CachePipelineModule RegisterCachePipelineBehaviors(Assembly assembly)
    {
        var typePairs = assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(CachePipelineAttribute), false).Any())
            .Select(type =>
            {

                var hasKeyInterface = RegisterEntitiesHelper.GetGenericInterface(type, typeof(IRequest<>));
                var responseType = hasKeyInterface!.GetGenericArguments()[0];
                return new
                {
                    RequestType = type,
                    ResponseType = responseType
                };
            })
            .ToList();

        foreach (var typePair in typePairs)
        {
            var interfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(typePair.RequestType, typePair.ResponseType);
            var pipelineBehaviorType = typeof(CachePipelineBehavior<,>).MakeGenericType(typePair.RequestType, typePair.ResponseType);
            Services.AddTransient(interfaceType, pipelineBehaviorType);
        }

        return this;
    }
}
