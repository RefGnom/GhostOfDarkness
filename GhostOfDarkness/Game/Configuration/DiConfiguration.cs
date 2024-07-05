using System;

namespace Game.Configuration;

public static class DiConfiguration
{
    private static IServiceProvider serviceProvider;

    public static IServiceProvider ServiceProvider
    {
        get => serviceProvider;
        set
        {
            if (serviceProvider is not null)
            {
                throw new InvalidOperationException("Service provider можно установить только 1 раз");
            }

            serviceProvider = value;
        }
    }
}