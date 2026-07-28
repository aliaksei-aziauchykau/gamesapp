using System.Reflection;

namespace BGP.Auth.Domain;

public static class AuthDomainAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
