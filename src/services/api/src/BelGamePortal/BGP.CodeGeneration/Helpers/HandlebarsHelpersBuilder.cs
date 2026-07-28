using System;
using System.Collections.Generic;
using System.Linq;
using HandlebarsDotNet;

namespace BGP.CodeGeneration.Builder;

public class HandlebarsHelpersBuilder
{
    public static HandlebarsHelpersBuilder Create()
    {
        return new HandlebarsHelpersBuilder();
    }

    public HandlebarsHelpersBuilder WithDashToPascalCaseFn()
    {
        Handlebars.RegisterHelper("dashToPascalCase", (writer, context, parameters) =>
        {
            if (parameters.Length > 0 && parameters[0] != null)
            {
                string input = parameters[0].ToString();

                // Split the input string by dashes
                var words = input.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                // Capitalize the first letter of each word and concatenate them
                string result = string.Concat(words.Select(word =>
                {
                    if (word.Length == 0) return string.Empty;
                    return char.ToUpperInvariant(word[0]) + word.Substring(1);
                }));

                writer.WriteSafeString(result);
            }
            else
            {
                writer.WriteSafeString(string.Empty);
            }
        });

        return this;
    }

}
