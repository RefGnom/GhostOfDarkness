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
                var serviceDescriptor = CreateServiceDescriptor(interfaceType, typeInfo);
                if (serviceDescriptor != null)
                {
                    serviceCollection.Add(serviceDescriptor);
                }
            }

            var baseType = typeInfo.BaseType;

            if (baseType is null)
            {
                continue;
            }

            var serviceDescriptorBaseType = CreateServiceDescriptor(baseType, typeInfo);
            if (serviceDescriptorBaseType != null)
            {
                serviceCollection.Add(serviceDescriptorBaseType);
            }
        }
    }

    private static ServiceDescriptor? CreateServiceDescriptor(Type serviceType, Type implementationType)
    {
        var implementationUsageAttribute = implementationType.GetCustomAttribute<DiUsageAttribute>();
        var serviceUsageAttribute = serviceType.GetCustomAttribute<DiUsageAttribute>();

        if (serviceUsageAttribute is null)
        {
            return null;
        }

        var serviceLifetime = implementationUsageAttribute?.ServiceLifetime ?? serviceUsageAttribute.ServiceLifetime;
        return new ServiceDescriptor(serviceType, implementationType, serviceLifetime);
    }

    private static void AddCurrentAssembly()
    {
        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Core");
        AddAssembly(assembly);
    }
}