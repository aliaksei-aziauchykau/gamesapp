namespace BGP.Shared.Modules.EntityApi.Extensions;

public static class StringExtensions
{
    public static string ToEntityNameFromDto(this string str)
    {
        if (str.EndsWith("Dto"))
        {
            return str.Substring(0, str.Length - 3);
        }

        return str;
    }

    public static string ToEntityNameFromCreateDto(this string str)
    {
        if (str.EndsWith("CreateDto"))
        {
            return str.Substring(0, str.Length - 9);
        }

        return str;
    }

    public static string ToEntityCollectionsFromCreateDto(this string str)
    {
        return $"{str.ToEntityNameFromCreateDto()}s";
    }

    public static string ToEntityCollectionsFromDto(this string str)
    {
        return $"{str.ToEntityNameFromDto()}s";
    }

    public static string ToEntityCollections(this string str, EntityDtoType type)
    {
        return type switch
        {
            EntityDtoType.Entity => str.ToEntityCollections(),
            EntityDtoType.CreateDto => str.ToEntityCollectionsFromCreateDto(),
            EntityDtoType.Dto => str.ToEntityCollectionsFromDto(),
            _ => throw new System.NotImplementedException(),
        };
    }

    public static string ToEntityCollections(this string str)
    {
        return $"{str}s";
    }

    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}

