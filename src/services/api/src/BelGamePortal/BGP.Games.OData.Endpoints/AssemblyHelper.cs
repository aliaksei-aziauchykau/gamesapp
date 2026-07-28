using System.Reflection;

namespace BGP.Games.OData.Endpoints;

public static class GamesODataEndpointsAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
