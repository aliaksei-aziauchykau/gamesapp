using System.Reflection;

namespace BGP.Auth.Application;

public static class AuthODataEndpointsAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
