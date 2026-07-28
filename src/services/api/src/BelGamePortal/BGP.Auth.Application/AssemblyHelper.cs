using System.Reflection;

namespace BGP.Auth.Application;

public static class AuthApplicationAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
