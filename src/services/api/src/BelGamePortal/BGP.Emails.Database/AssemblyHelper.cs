using System.Reflection;

namespace BGP.Emails.Database;

public static class EmailsDatabaseAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
