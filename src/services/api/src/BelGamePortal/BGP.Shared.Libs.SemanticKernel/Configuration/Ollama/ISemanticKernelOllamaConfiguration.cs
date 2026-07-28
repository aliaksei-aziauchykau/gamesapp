namespace BGP.Shared.Libs.SemanticKernel.Configuration;

public interface ISemanticKernelOllamaConfiguration
{
    public string Model { get; init; }

    public Uri Uri { get; init; }

}
