using System.Reflection;

namespace BGP.Emails.Application;

public static class EmailsDomainAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
