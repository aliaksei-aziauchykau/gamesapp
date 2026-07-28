using BGP.Shared.Libs.SemanticKernel.Configuration;
using BGP.Shared.Modules.DotNetEnv;
using BGP.Shared.Modules.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.MongoDB;
using Microsoft.SemanticKernel.Connectors.Ollama;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace BGP.Shared.Consoles.SemanticKernel;

public class Examples
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

        Console.WriteLine("Plugins: {0}", kernel.Plugins.Count());
        Console.WriteLine(string.Join(", ", kernel.Plugins.Select(x => x.Name).ToList()));

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
                executionSettings: new OllamaPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() },
                kernel: kernel);

            // Print the results
            Console.WriteLine("Assistant > " + result);

            // Add the message from the agent to the chat history
            history.AddMessage(result.Role, result.Content ?? string.Empty);

        } while (!string.IsNullOrEmpty(userInput));
    }

    public static async Task SimpleChatWithOllamaAsync()
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

        await ConsoleChatAsync(history, chatCompletionService, kernel);
    }

    public static async Task SimpleChatWithOllamaAndMongoVectorsAsync()
    {
        var serviceCollection = new ServiceCollection();
        var configurationManager = new ConfigurationManager();

        DotNetEnvModule
            .Create()
            .ReadEnvFiles(configurationManager, "Local", "../../../");

        var connectionString = configurationManager["Mongodb:ConnectionString"] ?? throw new InvalidOperationException("Mongodb:ConnectionString is required");
        var dbName = configurationManager["Mongodb:DbName"] ?? throw new InvalidOperationException("Mongodb:DbName is required");

        SemanticKernelModule
            .Create()
            .AddServiceCollection(serviceCollection)
            .AddServices(configuration)
            .WithMondoDbVectors(connectionString, dbName);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var chatCompletionService = serviceProvider.GetRequiredService<IChatCompletionService>();
        var kernel = serviceProvider.GetRequiredService<Kernel>();
        var history = new ChatHistory();

        var vectorStore = serviceProvider.GetRequiredService<IVectorStore>();
        var collection = vectorStore.GetCollection<string, Hotel>("skhotels");

        var hotels = new[]
        {
            new Hotel
            {
                HotelId = "1",
                HotelName = "Hotel 1",
                Description = "This is a hotel",
                DescriptionEmbedding = new float[] { 0.1f, 0.2f, 0.3f, 0.4f }
            },
            new Hotel
            {
                HotelId = "2",
                HotelName = "Hotel 2",
                Description = "This is another hotel",
                DescriptionEmbedding = new float[] { 0.5f, 0.6f, 0.7f, 0.8f }
            }
        };

        await foreach (var number in collection.UpsertBatchAsync(hotels))
        {
            Console.WriteLine($"Number: {number}");
        }


        await ConsoleChatAsync(history, chatCompletionService, kernel);
    }

    private class Hotel
    {
        [VectorStoreRecordKey]
        public string HotelId { get; set; }

        [BsonElement("hotel_name")]
        [VectorStoreRecordData(IsFilterable = true)]
        public string HotelName { get; set; }

        [BsonElement("hotel_description")]
        [VectorStoreRecordData(IsFullTextSearchable = true)]
        public string Description { get; set; }

        [BsonElement("hotel_description_embedding")]
        [VectorStoreRecordVector(4, DistanceFunction.CosineSimilarity)]
        public ReadOnlyMemory<float>? DescriptionEmbedding { get; set; }
    }

}
