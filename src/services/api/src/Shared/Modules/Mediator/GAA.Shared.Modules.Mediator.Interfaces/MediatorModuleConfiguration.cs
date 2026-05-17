namespace GAA.Shared.Modules.Mediator.Interfaces;

public class MediatorModuleConfiguration
{
    public bool UsePipelineBehaviors { get; set; }
    public Type[]? PipelineBehaviors { get; set; }
}
