


using System;
using System.Linq;

namespace BGP.CodeGeneration.Builder;

public static class StringExtensions
{
    public static string DashToPascalCase(this string dashNotation)
    {
        if (string.IsNullOrEmpty(dashNotation))
            return dashNotation;

        // Split the string on dashes, removing any empty entries
        string[] words = dashNotation.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

        // Capitalize the first letter of each word and concatenate them
        return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1)));
    }

    public static string DashToCamelCase(this string dashNotation)
    {
        var pascal = dashNotation.DashToPascalCase();
        return char.ToLower(pascal[0]) + pascal.Substring(1);
    }

    public static bool ToBool(this string? str)
    {
        if (string.IsNullOrEmpty(str))
            return false;

        return str switch
        {
            "true" => true,
            "false" => false,
            _ => false
        };
    }
}
