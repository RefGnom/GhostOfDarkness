using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class DiUsageAttribute : Attribute
{
    public readonly ServiceLifetime ServiceLifetime;
    public DiUsageAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ServiceLifetime = serviceLifetime;
    }
}