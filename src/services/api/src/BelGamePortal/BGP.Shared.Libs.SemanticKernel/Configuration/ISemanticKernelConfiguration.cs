namespace BGP.Shared.Libs.SemanticKernel.Configuration;

public interface ISemanticKernelConfiguration
{
    public string Type { get; init; }

    public string Model { get; init; }

    public string? ApiKey { get; init; }

    public string OrgId { get; init; }
}
