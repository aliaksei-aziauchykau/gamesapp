using System.Reflection;

namespace BGP.Games.Application;

public static class GamesApplicationAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
