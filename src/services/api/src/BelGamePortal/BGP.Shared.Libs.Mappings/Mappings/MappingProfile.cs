using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;

namespace BGP.Shared.Libs.Mappings.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile(Assembly assemblyToScan)
    {
        //ApplyMappingsFromAssembly(assemblyToScan);
        ApplyFromAssembly(assemblyToScan, typeof(IMap), nameof(IMap.Mapping));
        ApplyFromAssembly(assemblyToScan, typeof(IMap<,>), nameof(IMap.Mapping));
        ApplyFromAssembly(assemblyToScan, typeof(IMapFrom<>), nameof(IMapFrom<object>.Mapping));
        ApplyFromAssembly(assemblyToScan, typeof(IMapTo<>), nameof(IMapTo<object>.Mapping));
        ApplyFromAssembly(assemblyToScan, typeof(IProjection<,>), nameof(IProjection<object, object>.Projection));

        // ApplyProjectionsFromAssembly(assemblyToScan);
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        ApplyFromAssembly(assembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.Mapping));
    }

    private void ApplyFromAssembly(Assembly assembly, Type interfType, string methodName)
    {

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == interfType || t == interfType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var pars = GetParameters(type);

            var instance = pars.Length > 0
                ? Activator.CreateInstance(type, pars)
                : Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(methodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(methodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }

    private object?[] GetParameters(Type type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        // Retrieve the first public constructor
        ConstructorInfo[] constructors = type.GetConstructors();

        if (constructors.Length == 0)
        {
            throw new MissingMethodException($"Type {type.FullName} does not have a public constructor.");
        }

        ConstructorInfo constructor = constructors[0];

        // Get constructor parameters
        ParameterInfo[] parameters = constructor.GetParameters();
        object?[] args = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            Type paramType = parameters[i].ParameterType;

            if (this.IsNullable(paramType))
            {
                args[i] = null;
            }
            else
            {
                // Assign default value for non-nullable types
                args[i] = GetDefault(paramType);
            }
        }

        // Create the instance
        return args;
    }

    private bool IsNullable(Type type)
    {
        if (!type.IsValueType)
            return true; // Reference types are nullable

        // Nullable value types
        return Nullable.GetUnderlyingType(type) != null;
    }

    private object? GetDefault(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }
}
