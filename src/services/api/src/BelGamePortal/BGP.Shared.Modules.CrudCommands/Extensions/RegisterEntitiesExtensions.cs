using System.Reflection;
using Autofac;
using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.CrudCommands.Extensions;
internal static class RegisterEntitiesExtensions
{
    public static IServiceCollection RegisterCrudCommands(this IServiceCollection services, Assembly domaninAssembly, Action<Type,Type> registerPhaseAction)
    {
        var entitiesTypes = domaninAssembly
         .GetTypes()
         .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IHasKey<>)))
         .ToList();

        var commandHandlersTypesSet = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IRequestHandler<,>)))
            .Select(commandHandlerType =>
            {
                var genericInterface = commandHandlerType.GetInterfaces().First(interf => interf.IsGenericType && interf.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                var commandTypeArgument = genericInterface.GetGenericArguments()[0];
                var returnTypeArgument = genericInterface.GetGenericArguments()[1];
                return new { commandHandlerType, commandTypeArgument, returnTypeArgument };
            })
            .ToList();

        foreach (var entityType in entitiesTypes)
        {
            foreach (var commandHandlerTypeSet in commandHandlersTypesSet)
            {
                var hasKeyInterface = RegisterEntitiesHelper.GetGenericInterface(entityType, typeof(IHasKey<>));
                var entityKeyType = hasKeyInterface!.GetGenericArguments()[0];
                var handlerType = RegisterEntitiesHelper.PossibleMakeGenericType(commandHandlerTypeSet.commandTypeArgument, entityType, entityKeyType);
                var returnType = RegisterEntitiesHelper.PossibleMakeGenericType(commandHandlerTypeSet.returnTypeArgument, entityType);

                Type[] args = { handlerType, returnType };
                var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(args);
                var implementationType = commandHandlerTypeSet.commandHandlerType.MakeGenericType(entityType, entityKeyType);

                services.AddScoped(interfaceType, implementationType);

                registerPhaseAction(entityType, entityKeyType);
            }
        }

        return services;
    }

     public static IServiceCollection RegisterGenericCommand(this IServiceCollection services, Type entityType, Type commandType, Type commandHandler, Type returnType)
    {
        var hasKeyInterface = RegisterEntitiesHelper.GetGenericInterface(entityType, typeof(IHasKey<>));
        var entityKeyType = hasKeyInterface!.GetGenericArguments()[0];
        var entityKeyPairType = new[] { entityType, entityKeyType };

        var implementationType = commandHandler.MakeGenericType(entityKeyPairType);
        var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(new[] { commandType.MakeGenericType(entityKeyPairType), returnType });
        services.AddScoped(interfaceType, implementationType);

        return services;
    }
}
