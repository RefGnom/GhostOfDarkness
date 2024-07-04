using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyInjection;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class DiScopeAttribute : Attribute
{
    public readonly ServiceLifetime ServiceLifetime;

    public DiScopeAttribute(ServiceLifetime lifetime)
    {
        ServiceLifetime = lifetime;
    }
}