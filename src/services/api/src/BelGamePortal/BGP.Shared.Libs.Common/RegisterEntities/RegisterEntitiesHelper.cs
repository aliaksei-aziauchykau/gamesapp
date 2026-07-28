namespace BGP.Shared.Libs.Common;

public class RegisterEntitiesHelper
{
    public static Type? GetGenericInterface(Type type, Type interfToFind)
    {
        var interfaceToReturn = type.GetInterfaces().FirstOrDefault(interf => interf.IsGenericType && interf.GetGenericTypeDefinition() == interfToFind);
        return interfaceToReturn;
    }

    public static bool HasInterface(Type type, Type interfToFind)
    {
        var isExist = type.GetInterfaces().Contains(interfToFind);
        return isExist;
    }

    public static bool HasGenericInterface(Type type, Type interfToFind)
    {
        var isExist = type.GetInterfaces().Any(interf => interf.IsGenericType && interf.GetGenericTypeDefinition() == interfToFind);
        return isExist;
    }

    public static bool HasBaseType(Type type, Type baseTypeToFind)
    {
        var isExist = type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseTypeToFind;
        return isExist;
    }

    public static Type PossibleMakeGenericType(Type type, params Type[] argumentOfGenericType)
    {
        if (type.IsInterface && !type.IsGenericType) return type;

        if (!type.IsInterface && !type.IsGenericType) return argumentOfGenericType[0];

        return type.GetGenericTypeDefinition().MakeGenericType(argumentOfGenericType);
    }

    public static bool HasGenericInterfaceAndNeccessaryCountOfTypeArguments(Type type, Type interfToFind, int count)
    {
        var interfaceToReturn = GetGenericInterface(type, interfToFind);

        if (interfaceToReturn == null) return false;

        var firstTypeArgument = interfaceToReturn.GetGenericArguments()[0];

        if (firstTypeArgument.IsGenericType && firstTypeArgument.GetGenericArguments().Length == count) return true;

        return false;
    }
}
