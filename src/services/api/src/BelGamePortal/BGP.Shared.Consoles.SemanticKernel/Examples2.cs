using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using BGP.Shared.Libs.SemanticKernel.Configuration;
using BGP.Shared.Modules.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;

namespace BGP.Shared.Consoles.SemanticKernel;

public class Examples2
{
    private readonly static ISemanticKernelOllamaConfiguration configuration = new SemanticKernelOllamaConfiguration
    {
        Model = "llama3.2:3b",
        Uri = new Uri("http://localhost:11434")
    };

    private static async Task ConsoleChatAsync(ChatHistory history, IChatCompletionService chatCompletionService, Kernel kernel)
    {
        // Initiate a back-and-forth chat
        string? userInput;
        do
        {
            // Collect user input
            Console.Write("User > ");
            userInput = Console.ReadLine();

            // Add user input
            history.AddUserMessage(userInput);

            // Get the response from the AI
            var result = await chatCompletionService.GetChatMessageContentAsync(
                history,
                kernel: kernel);

            // Print the results
            Console.WriteLine("Assistant > " + result);

            // Add the message from the agent to the chat history
            history.AddMessage(result.Role, result.Content ?? string.Empty);
        } while (!string.IsNullOrEmpty(userInput));
    }


    public static async Task RunAsync()
    {

        var serviceCollection = new ServiceCollection();

        SemanticKernelModule
            .Create()
            .AddServiceCollection(serviceCollection)
            .AddServices(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var chatCompletionService = serviceProvider.GetRequiredService<IChatCompletionService>();
        var kernel = serviceProvider.GetRequiredService<Kernel>();
        var history = new ChatHistory();

        Console.WriteLine("Creating kernel...");
        IKernelBuilder builder = Kernel.CreateBuilder();

        Kernel toolKernel = kernel.Clone();
        toolKernel.Plugins.AddFromType<ClipboardAccess>();

        Console.WriteLine("Defining agents...");

        const string ManagerName = "Manager";
        const string ExecuterName = "Executer";

        ChatCompletionAgent manager =
            new()
            {
                Name = ManagerName,
                Instructions =
                    """
                    You are a manager and your task is to decompose the task from user into actionable steps or subtasks to future execution. 
                    If you can't decompose the task, you can ask the user for more information. At the same time, the format of performing task
                    should be in followed format:

                    1. User input.
                    1.1. Subtask 1. Title.
                    1.2. Block of instructions.
                    1.2.1. Instruction 1.
                    1.2.2. Instruction 2.
                    1.2.3. Instruction N.
                    1.3. Role of Executer. For example - "Developer", "QA", "DevOps", etc.
                    1.4. Current Status - "Not Started", "In Progress", "Completed", etc.
                    2.1. Subtask 2. Title.
                    2.2. Block of instructions.
                    2.2.1. Instruction 1.
                    2.2.2. Instruction 2.
                    2.2.3. Instruction N.
                    2.3. Role of Executer. For example - "Developer", "QA", "DevOps", etc.
                    2.4. Current Status - "Not Started", "In Progress", "Completed", etc.

                    In case if you see the decomposed list of tasks and it has "Done" in the end, then you should respond with "Tasks Completed".

                    RULES:
                    - Strictly follow the format when you provide decomposition for tasks. In case if for performing it requires more substeps, you can added.
                    - Do not ask and provide any additional information. Only the formatted list of decomposed tasks or completion flow.
                    - Current list of available roles are: "FileSaver", "Developer", "QA".
                    - If there is no role which fits the task, you can report to user that is not possible to achieve the task by using word "impossible".
                    """,
                Kernel = toolKernel,
                Arguments = new KernelArguments(new OllamaPromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() })
            };

        ChatCompletionAgent executer =
            new()
            {
                Name = ExecuterName,
                Instructions =
                    """
                    You are a executer and your task is to review the task list from the manager and for each subtask change the status to "In Progress".
                    After that, you should write word "Done" in the end of the task list.
                    """,
                Kernel = kernel,
            };

        KernelFunction selectionFunction =
            AgentGroupChat.CreatePromptFunctionForStrategy(
                $$$"""
                Examine the provided History. You must decide who should respond next.

                Choose only from these participants:
                - {{{ManagerName}}}
                - {{{ExecuterName}}}

                Always follow these rules when choosing the next participant:
                - If History is user input, then it {{{ManagerName}}}'s turn.
                - If History is list of decomposed task, then it is {{{ExecuterName}}}'s turn.
                - If History is list of decomposed task with done in the end, then it is {{{ManagerName}}}'s turn.

                History:
                {{$history}}
                """,
                safeParameterNames: "history");

        const string TerminationToken = "impossible";

        KernelFunction terminationFunction =
            AgentGroupChat.CreatePromptFunctionForStrategy(
                $$$"""
                Examine the History and determine whether the content has been deemed satisfactory.
                If content is a "impossible" single word without explanation, then respond with words "Tasks Completed".
                If content is a decomposed tasks,  then respond with word "Tasks Completed".

                History:
                {{$history}}
                """,
                safeParameterNames: "history");

        ChatHistoryTruncationReducer historyReducer = new(1);

        AgentGroupChat chat =
            new(manager, executer)
            {
                ExecutionSettings = new AgentGroupChatSettings
                {
                    SelectionStrategy =
                        new KernelFunctionSelectionStrategy(selectionFunction, kernel)
                        {
                            // Always start with the editor agent.
                            InitialAgent = manager,
                            // Save tokens by only including the final response
                            HistoryReducer = historyReducer,
                            // The prompt variable name for the history argument.
                            HistoryVariableName = "history",
                            // Returns the entire result value as a string.
                            ResultParser = (result) => result.GetValue<string>() ?? manager.Name
                        },
                    TerminationStrategy =
                        new KernelFunctionTerminationStrategy(terminationFunction, kernel)
                        {
                            // Only evaluate for editor's response
                            Agents = [manager],
                            // Save tokens by only including the final response
                            HistoryReducer = historyReducer,
                            // The prompt variable name for the history argument.
                            HistoryVariableName = "lastmessage",
                            // Limit total number of turns
                            MaximumIterations = 12,
                            // Customer result parser to determine if the response is "yes"
                            ResultParser = (result) => result.GetValue<string>()?.Contains("Done", StringComparison.OrdinalIgnoreCase) ?? false
                        }
                }
            };

        Console.WriteLine("Ready!");

        bool isComplete = false;
        do
        {
            Console.WriteLine();
            Console.Write("> ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            input = input.Trim();
            if (input.Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                isComplete = true;
                break;
            }

            if (input.Equals("RESET", StringComparison.OrdinalIgnoreCase))
            {
                await chat.ResetAsync();
                Console.WriteLine("[Converation has been reset]");
                continue;
            }

            chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, input));

            chat.IsComplete = false;

            try
            {
                await foreach (ChatMessageContent response in chat.InvokeAsync())
                {
                    Console.WriteLine();
                    Console.WriteLine($"{response.AuthorName.ToUpperInvariant()}:{Environment.NewLine}{response.Content}");
                }
            }
            catch (HttpOperationException exception)
            {
                Console.WriteLine(exception.Message);
                if (exception.InnerException != null)
                {
                    Console.WriteLine(exception.InnerException.Message);
                    if (exception.InnerException.Data.Count > 0)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(exception.InnerException.Data, new JsonSerializerOptions() { WriteIndented = true }));
                    }
                }
            }
        } while (!isComplete);
    }

    private sealed class ClipboardAccess
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

}
