using System;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class PowerShellExecutor
{
    /// <summary>
    /// Executes a PowerShell script from a specified file path.
    /// </summary>
    /// <param name="scriptFullPath">The full path to the PowerShell script (.ps1) file.</param>
    /// <returns>Returns the output of the script execution as a string. If an error occurs, returns the error message.</returns>
    public static async Task<string> ExecuteScriptAsync(string scriptPath, Dictionary<string, object> parameters, string baseProjectPath = "./BGP.CodeGeneration")
    {
        var scriptFullPath = Path.Combine(baseProjectPath, scriptPath);

        // Check if the file exists
        if (!File.Exists(scriptFullPath))
        {
            throw new InvalidOperationException($"Error: Script file not found at {scriptFullPath}");
        }

        // Read the content of the PowerShell script
        string scriptContent = await File.ReadAllTextAsync(scriptFullPath);

        // Create a PowerShell instance
        using (PowerShell psInstance = PowerShell.Create())
        {
            psInstance.AddScript(scriptContent);  // Add the script content to the PowerShell instance

            // Add arguments to the script
            foreach (var param in parameters)
            {
                psInstance.AddParameter(param.Key, param.Value);
            }

            try
            {
                // Invoke the script
                PSDataCollection<PSObject> results = await psInstance.InvokeAsync();

                // Collect the results into a string
                if (psInstance.Streams.Error.Count > 0)
                {
                    // If there are any errors, return them
                    throw new InvalidOperationException($"Error: {psInstance.Streams.Error[0].ToString()}");
                }

                string output = "";
                foreach (var result in results)
                {
                    output += result.ToString() + Environment.NewLine;
                }

                return output;
            }
            catch (Exception ex)
            {
                // Catch and return any exceptions
                throw new InvalidOperationException($"Exception occurred: {ex.Message}");
            }
        }
    }
}
