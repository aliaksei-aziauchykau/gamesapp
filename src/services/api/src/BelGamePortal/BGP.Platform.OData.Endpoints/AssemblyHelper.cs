using System.Reflection;

namespace BGP.Platform.ODataEndpoints;

public static class PlatformODataEndpointsAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
