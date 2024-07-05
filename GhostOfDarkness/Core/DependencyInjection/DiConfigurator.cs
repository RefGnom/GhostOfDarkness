using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection;

public static class DiConfigurator
{
    private static readonly List<Assembly> assemblies;

    static DiConfigurator()
    {
        assemblies = new List<Assembly>();
        OnConfigure = _ => { };
    }

    public static Action<IServiceCollection> OnConfigure { get; set; }

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

        OnConfigure(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }

    private static void RegisterAllTypesFromAssembly(IServiceCollection serviceCollection, Assembly assembly)
    {
        foreach (var typeInfo in assembly.DefinedTypes)
        {
            var skipType = typeInfo.IsInterface || typeInfo.IsEnum || typeInfo.IsAbstract;
            if (skipType)
            {
                continue;
            }

            var diUsageAttribute = typeInfo.GetCustomAttribute<DiUsageAttribute>();
            var interfaces = typeInfo.ImplementedInterfaces.Where(x => x.Assembly == assembly).ToArray();

            if (interfaces.Length == 0 && diUsageAttribute is not null)
            {
                var serviceLifetime = diUsageAttribute.ServiceLifetime;
                var serviceDescriptor = new ServiceDescriptor(typeInfo, typeInfo, serviceLifetime);
                serviceCollection.Add(serviceDescriptor);
            }

            foreach (var interfaceType in interfaces)
            {
                var interfaceDiUsageAttribute = interfaceType.GetCustomAttribute<DiUsageAttribute>();

                if (diUsageAttribute is not null || interfaceDiUsageAttribute is not null)
                {
                    var serviceLifetime = diUsageAttribute?.ServiceLifetime ?? interfaceDiUsageAttribute!.ServiceLifetime;
                    var serviceDescriptor = new ServiceDescriptor(interfaceType, typeInfo, serviceLifetime);
                    serviceCollection.Add(serviceDescriptor);
                }
            }
        }
    }

    private static void AddCurrentAssembly()
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Core");
        AddAssembly(assembly);
    }
}