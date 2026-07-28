using System.Reflection;

namespace BGP.Emails.OData.Api;

public static class EmailsODataEndpointsAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
