using System.Reflection;

namespace BGP.Shared.Modules.CrudCommands.Config;

public class CrudCommandsByEntityModuleConfig : ICrudCommandsByEntityModuleConfig
{
    public Type EntityType { get; set; }
}
