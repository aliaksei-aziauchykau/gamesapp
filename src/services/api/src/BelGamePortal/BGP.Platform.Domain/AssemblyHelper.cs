using System.Reflection;

namespace BGP.Platform.Domain;

public static class PlatformDomainAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
