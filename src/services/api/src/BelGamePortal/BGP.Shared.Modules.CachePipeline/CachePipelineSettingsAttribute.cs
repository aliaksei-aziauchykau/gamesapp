
namespace BGP.Shared.Modules.CachePipeline;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CachePipelineSettingsAttribute : Attribute
{
    public int ExpirationInSeconds { get; }

    public CachePipelineSettingsAttribute(int expirationInSeconds)
    {
        ExpirationInSeconds = expirationInSeconds;
    }
}
