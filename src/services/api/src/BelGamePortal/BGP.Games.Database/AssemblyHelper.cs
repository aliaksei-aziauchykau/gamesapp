using System.Reflection;

namespace BGP.Games.Database;

public static class GamesDatabaseAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
