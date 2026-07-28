using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace BGP.Shared.Libs.Api.Middlewares;

public class CamelToPascalCaseMiddleware
{
    private readonly RequestDelegate _next;

    public CamelToPascalCaseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentType != null && context.Request.ContentType.Contains("application/json") && context.Request.Method == "PATCH")
        {
            context.Request.EnableBuffering(); // Enable reading the body multiple times

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset the stream position

                var transformedJson = ConvertCamelToPascal(body);

                // Replace the request body with the PascalCase JSON
                var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(transformedJson));
                context.Request.Body = memoryStream;
                context.Request.Body.Position = 0;
            }
        }

        await _next(context);
    }

    private string ConvertCamelToPascal(string json)
    {
        JObject jsonObject = JObject.Parse(json);
        var transformedObject = TransformToPascalCase(jsonObject);
        return JsonConvert.SerializeObject(transformedObject, Formatting.Indented);
    }

    private JToken TransformToPascalCase(JToken token)
    {
        if (token is JObject)
        {
            var obj = new JObject();
            foreach (var property in (JObject)token)
            {
                string pascalCaseKey = ConvertToPascalCase(property.Key);
                obj[pascalCaseKey] = TransformToPascalCase(property.Value);
            }
            return obj;
        }
        else if (token is JArray)
        {
            var array = new JArray();
            foreach (var item in (JArray)token)
            {
                array.Add(TransformToPascalCase(item));
            }
            return array;
        }
        else
        {
            return token; // For primitive types like string, number, bool
        }
    }

    private static string ConvertToPascalCase(string camelCase)
    {
        if (string.IsNullOrEmpty(camelCase))
            return camelCase;

        return char.ToUpper(camelCase[0], CultureInfo.InvariantCulture) + camelCase.Substring(1);
    }

    private object TransformKeysToPascalCase(object obj)
    {
        if (obj is Dictionary<string, object> dict)
        {
            var newDict = new Dictionary<string, object>();

            foreach (var kvp in dict)
            {
                // Transform the key to PascalCase
                var pascalCaseKey = char.ToUpper(kvp.Key[0]) + kvp.Key.Substring(1);

                // Recursively transform the value
                newDict[pascalCaseKey] = TransformKeysToPascalCase(kvp.Value);
            }

            return newDict;
        }
        else if (obj is List<object> list)
        {
            // Recursively transform each item in the list
            return list.Select(TransformKeysToPascalCase).ToList();
        }
        else
        {
            // Base case: return the value as-is (e.g., primitives)
            return obj;
        }
    }
}
