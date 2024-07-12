using Game.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Controllers.InputServices;

public static class Input
{
    private static IMouseService mouseService;
    private static IKeyboardService keyboardService;

    public static IMouseService MouseService => mouseService ??= DiConfiguration.ServiceProvider.GetService<IMouseService>();

    public static IKeyboardService KeyboardService => keyboardService ??= DiConfiguration.ServiceProvider.GetService<IKeyboardService>();
}