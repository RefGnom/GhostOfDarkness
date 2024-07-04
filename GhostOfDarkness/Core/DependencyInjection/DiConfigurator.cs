using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection;

public static class DiConfigurator
{
    private static readonly List<Assembly> assemblies;

    static DiConfigurator()
    {
        assemblies = new List<Assembly>();
    }

    public static void AddAssembly(Assembly assembly)
    {
        assemblies.Add(assembly);
    }

    public static IServiceProvider Configure()
    {
        var serviceCollection = new ServiceCollection();
        AddCurrentAssembly();

        foreach (var assembly in assemblies)
        {
            RegisterAllTypesFromAssembly(serviceCollection, assembly);
        }

        return serviceCollection.BuildServiceProvider();
    }

    private static void RegisterAllTypesFromAssembly(IServiceCollection serviceCollection, Assembly assembly)
    {
        foreach (var typeInfo in assembly.DefinedTypes)
        {
            var skipType = typeInfo.IsInterface || typeInfo.IsEnum || typeInfo.IsAbstract || typeInfo.GetCustomAttribute<DiUsageAttribute>() is null;
            if (skipType)
            {
                continue;
            }

            var diScopeAttribute = typeInfo.GetCustomAttribute<DiScopeAttribute>();
            var serviceLifetime = diScopeAttribute?.ServiceLifetime ?? ServiceLifetime.Singleton;
            var interfaces = typeInfo.ImplementedInterfaces.Where(x => x.Assembly == assembly).ToArray();

            if (interfaces.Length == 0)
            {
                var serviceDescriptor = new ServiceDescriptor(typeInfo, typeInfo, serviceLifetime);
                serviceCollection.Add(serviceDescriptor);
            }

            foreach (var interfaceType in interfaces)
            {
                var interfaceDiScopeAttribute = interfaceType.GetCustomAttribute<DiScopeAttribute>();
                var interfaceServiceLifetime = interfaceDiScopeAttribute?.ServiceLifetime ?? ServiceLifetime.Singleton;

                serviceLifetime = diScopeAttribute is null ? interfaceServiceLifetime : serviceLifetime;
                var serviceDescriptor = new ServiceDescriptor(interfaceType, typeInfo, serviceLifetime);
                serviceCollection.Add(serviceDescriptor);
            }
        }
    }

    private static void AddCurrentAssembly()
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Core");
        AddAssembly(assembly);
    }
}