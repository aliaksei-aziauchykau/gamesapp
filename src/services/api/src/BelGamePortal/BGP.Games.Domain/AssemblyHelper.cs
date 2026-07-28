using System.Reflection;

namespace BGP.Games.Domain;

public static class GamesDomainAssemblyHelper
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
