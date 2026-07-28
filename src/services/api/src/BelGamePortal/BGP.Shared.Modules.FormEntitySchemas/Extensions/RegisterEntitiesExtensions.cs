using System;
using System.Linq;
using System.Reflection;
using BGP.Shared.Libs.Common;
using BGP.Shared.Libs.Domain.Entities;
using BGP.Shared.Libs.FormEntitySchemas.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.FormEntitySchemas.Extensions;

public static class RegisterEntitiesExtensions
{
    public static IServiceCollection RegisterFormSchemaCommands(this IServiceCollection services, Assembly domainAssembly)
    {
        var entitiesTypes = domainAssembly
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
                var handlerType = RegisterEntitiesHelper.PossibleMakeGenericType(commandHandlerTypeSet.commandTypeArgument, entityType);

                Type[] args = { handlerType, commandHandlerTypeSet.returnTypeArgument };
                var interfaceType = typeof(IRequestHandler<,>).MakeGenericType(args);
                var implementationType = commandHandlerTypeSet.commandHandlerType.MakeGenericType(entityType);

                services.AddScoped(interfaceType, implementationType);
            }
        }

        return services;
    }

    public static IServiceCollection RegisterFormSchemaProviders(this IServiceCollection services, Assembly targetAssembly)
    {
        var providerTypes = targetAssembly
          .GetTypes()
          .Where(type => RegisterEntitiesHelper.HasGenericInterface(type, typeof(IFormEntitySchemaConfiguration<>)))
          .Select(type =>
          {
              var genericInterface = RegisterEntitiesHelper.GetGenericInterface(type, typeof(IFormEntitySchemaConfiguration<>))!;
              return new { type, genericArgumentType = genericInterface.GetGenericArguments()[0] };
          })
          .ToList();

        foreach (var providerType in providerTypes)
        {
            services.RegisterProvider(providerType.type, providerType.genericArgumentType);
        }

        return services;
    }


    private static void RegisterProvider(this IServiceCollection services, Type type, Type genericArgumentType)
    {
        var interfaceType = typeof(IFormEntitySchemaConfiguration<>).MakeGenericType(genericArgumentType);

        services.AddScoped(interfaceType, type);
    }
}
