using System.Reflection;

namespace BGP.Identity.Database;

public static class IdentityDatabaseAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
