using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration;

public class ArgumentParser
{
    public static Dictionary<string, string> ParseArguments(string[] argsCommands)
    {
        var argumentString = string.Join(" ", argsCommands);

        var result = new Dictionary<string, string>();
        var args = argumentString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < args.Length; i++)
        {
            // If the current item starts with "--", treat it as a key
            if (args[i].StartsWith("--"))
            {
                // Make sure there is a value following the key
                if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
                {
                    var key = args[i].Substring(2);  // Remove "--" from the key
                    var value = args[i + 1];         // The next item is the value
                    result[key] = value;
                    i++; // Skip the value, as it has been processed
                }
            }
        }

        return result;
    }


}
