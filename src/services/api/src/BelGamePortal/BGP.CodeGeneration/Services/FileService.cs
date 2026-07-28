using System;
using System.IO;
using System.Threading.Tasks;

namespace BGP.CodeGeneration;

public static class FileService
{
    public static async Task<string> ReadFileAsync(string path)
    {
        try
        {
            return await File.ReadAllTextAsync(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            throw;
        }
    }

    public static async Task WriteFileAsync(string path, string content)
    {
        try
        {
            await File.WriteAllTextAsync(path, content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing file: {ex.Message}");
        }
    }


}
