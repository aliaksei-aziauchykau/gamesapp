
using System.Reflection;

namespace BGP.Shared.Libs.Common.Helpers;

public static class AssemblyExtensions
{
    public static Assembly[] Merge(this Assembly assembly, params Assembly[] assemblies)
    {
        Assembly[] allAssemblies = [assembly];
        allAssemblies.Concat(assemblies);

        return allAssemblies;
    }

    public static void ForEach(this Assembly[] assemblies, Action<Assembly> action)
    {
        Array.ForEach(assemblies, action);
    }
}

