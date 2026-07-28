using System.Reflection;
using Autofac;
using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.Mappings.Mappings;
using BGP.Shared.Modules.AutoMapper.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.MapCommands;
internal static class RegisterEntitiesExtension
{
    public static IServiceCollection RegisterMapCommands(this IServiceCollection services, Assembly targetAssembly)
    {
        var mapToSets = targetAssembly
          .GetTypes()
          .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IMapTo<>)))
          .Select(type =>
          {
              var genericInterface = RegisterEntitiesHelper.GetGenericInterface(type, typeof(IMapTo<>))!;
              return new { sourceType = type, destinationType = genericInterface.GetGenericArguments()[0] };
          })
          .ToList();

        var mapFromSets = targetAssembly
          .GetTypes()
          .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IMapFrom<>)))
          .Select(type =>
          {
              var genericInterface = RegisterEntitiesHelper.GetGenericInterface(type, typeof(IMapFrom<>))!;
              return new { sourceType = genericInterface.GetGenericArguments()[0], destinationType = type };
          })
          .ToList();

        var mapSetGenerics = targetAssembly
          .GetTypes()
          .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IMap<,>)))
          .Select(type =>
          {
              var genericInterface = RegisterEntitiesHelper.GetGenericInterface(type, typeof(IMap<,>))!;
              return new { sourceType = genericInterface.GetGenericArguments()[0], destinationType = genericInterface.GetGenericArguments()[1] };
          })
          .ToList();

        var mapSets = mapToSets.Concat(mapFromSets).Concat(mapSetGenerics).ToList();

        foreach (var mapSet in mapSets)
        {
            services
                .RegisterMapCommand(mapSet.sourceType, mapSet.destinationType)
                .RegisterPatchCommand(mapSet.sourceType, mapSet.destinationType)
                .RegisterMapListCommand(mapSet.sourceType, mapSet.destinationType);
        }

        return services;
    }

    private static IServiceCollection RegisterMapCommand(this IServiceCollection services, Type sourceType, Type destinationType)
    {
        var handlerType = RegisterEntitiesHelper.PossibleMakeGenericType(typeof(MapCommand<,>), sourceType, destinationType);

        Type[] intefaceArgs = { handlerType, destinationType };
        var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(intefaceArgs);
        Type[] implementationArgs = { sourceType, destinationType };
        var implementationType = typeof(MapCommandHandler<,>).MakeGenericType(implementationArgs);

        services.AddScoped(interfaceType, implementationType);

        return services;
    }

    private static IServiceCollection RegisterPatchCommand(this IServiceCollection services, Type sourceType, Type destinationType)
    {
        var handlerType = RegisterEntitiesHelper.PossibleMakeGenericType(typeof(PatchCommand<,>), sourceType, destinationType);

        Type[] intefaceArgs = { handlerType, destinationType };
        var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(intefaceArgs);
        Type[] implementationArgs = { sourceType, destinationType };
        var implementationType = typeof(PatchCommandHandler<,>).MakeGenericType(implementationArgs);

        services.AddScoped(interfaceType, implementationType);

        return services;
    }

    private static IServiceCollection RegisterMapListCommand(this IServiceCollection services, Type sourceType, Type destinationType)
    {
        var handlerType = RegisterEntitiesHelper.PossibleMakeGenericType(typeof(MapListCommand<,>), sourceType, destinationType);
        var returnType = RegisterEntitiesHelper.PossibleMakeGenericType(typeof(IList<>), destinationType);

        Type[] intefaceArgs = { handlerType, returnType };
        var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(intefaceArgs);
        Type[] implementationArgs = { sourceType, destinationType };
        var implementationType = typeof(MapListCommandHandler<,>).MakeGenericType(implementationArgs);

        services.AddScoped(interfaceType, implementationType);

        return services;
    }
}
