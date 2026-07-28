namespace BGP.Shared.Libs.SemanticKernel.Configuration;

public class SemanticKernelOllamaConfiguration : ISemanticKernelOllamaConfiguration
{
    public required string Model { get; init; }

    public required Uri Uri { get; init; }
}
