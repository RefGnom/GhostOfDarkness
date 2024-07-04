using System.Reflection;

namespace Core;

public static class AssemblyProvider
{
    public static Assembly CoreAssembly => AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Core");
}