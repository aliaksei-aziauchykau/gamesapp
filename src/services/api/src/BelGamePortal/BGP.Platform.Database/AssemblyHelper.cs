using System.Reflection;

namespace BGP.Platform.Database;

public static class PlatformDatabaseAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
