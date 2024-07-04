using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Game.EntryPoint;

public static class DiConfigurator
{
    public static IServiceProvider Configure()
    {
        var serviceCollection = new ServiceCollection();
        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(x => x.GetName().Name == "Game");
        RegisterAllTypesFromAssembly(serviceCollection, assembly);

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

            var interfaces = typeInfo.ImplementedInterfaces.Where(x => x.Assembly == assembly).ToArray();

            if (interfaces.Length == 0)
            {
                var serviceDescriptor = new ServiceDescriptor(typeInfo, typeInfo, ServiceLifetime.Singleton);
                serviceCollection.Add(serviceDescriptor);
            }

            foreach (var interfaceType in interfaces)
            {
                var serviceDescriptor = new ServiceDescriptor(interfaceType, typeInfo, ServiceLifetime.Singleton);
                serviceCollection.Add(serviceDescriptor);
            }
        }
    }
}