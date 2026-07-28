
using System.Reflection;

namespace BGP.Shared.Libs.Common.Helpers;

public class AssemblyHelper
{
    public static Assembly[] Merge(Assembly assembly, params Assembly[] assemblies)
    {
        Assembly[] allAssemblies = [assembly];
        allAssemblies.Concat(assemblies);

        return allAssemblies;
    }
}
