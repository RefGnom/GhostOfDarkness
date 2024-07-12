using System;

namespace Game.Controllers.InputServices;

public static class Input
{
    private static IMouseService mouseService;
    private static IKeyboardService keyboardService;

    public static IMouseService MouseService
    {
        get
        {
            if (mouseService is null)
            {
                throw new InvalidOperationException("Mouse service must be initialized");
            }

            return mouseService;
        }
        set
        {
            if (mouseService is not null)
            {
                throw new InvalidOperationException("Mouse service must be initialized once");
            }

            mouseService = value;
        }
    }

    public static IKeyboardService KeyboardService
    {
        get
        {
            if (keyboardService is null)
            {
                throw new InvalidOperationException("Keyboard service must be initialized");
            }

            return keyboardService;
        }
        set
        {
            if (keyboardService is not null)
            {
                throw new InvalidOperationException("Keyboard service must be initialized once");
            }

            keyboardService = value;
        }
    }
}