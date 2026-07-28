using System.Reflection;

namespace BGP.Emails.Application;

public static class EmailsApplicationAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
