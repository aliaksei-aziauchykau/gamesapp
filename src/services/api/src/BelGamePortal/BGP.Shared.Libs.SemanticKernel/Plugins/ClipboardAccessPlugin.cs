using System.ComponentModel;
using System.Diagnostics;
using Microsoft.SemanticKernel;

namespace BGP.Shared.Libs.SemanticKernel.Plugins;

public class ClipboardAccessPlugin
{
    [KernelFunction]
    [Description("Copies the provided content to the clipboard.")]
    public static void SetClipboard(string content)
    {

        Console.WriteLine("Copied to clipboard: " + content);

        if (string.IsNullOrWhiteSpace(content))
        {
            return;
        }

        using Process clipProcess = Process.Start(
            new ProcessStartInfo
            {
                FileName = "clip",
                RedirectStandardInput = true,
                UseShellExecute = false,
            });

        clipProcess.StandardInput.Write(content);
        clipProcess.StandardInput.Close();
    }

}
