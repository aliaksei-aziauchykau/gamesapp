namespace BGP.Shared.Libs.SemanticKernel.Configuration;

public class SemanticKernelConfiguration : ISemanticKernelConfiguration
{
    public required string Type { get; init; }

    public required string Model { get; init; }

    public string? ApiKey { get; init; }

    public string? OrgId { get; init; }
}
