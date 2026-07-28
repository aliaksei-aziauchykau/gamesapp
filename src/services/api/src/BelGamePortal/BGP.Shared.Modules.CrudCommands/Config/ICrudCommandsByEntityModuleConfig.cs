using System.Reflection;

namespace BGP.Shared.Modules.CrudCommands.Config;

public interface ICrudCommandsByEntityModuleConfig
{
    Type EntityType { get; }
}
