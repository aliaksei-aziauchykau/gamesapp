using System.Reflection;

namespace BGP.Platform.Application;

public static class PlatformApplicationAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
