using Microsoft.Xna.Framework.Input;

namespace game.Input;

internal static class KeyboardController
{
    private static KeyboardState currentState;
    private static KeyboardState previousState;

    public static bool IsSingleKeyDown(Keys key)
    {
        return currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);
    }

    public static bool IsSingleKeyUp(Keys key)
    {
        return currentState.IsKeyUp(key) && !previousState.IsKeyUp(key);
    }

    public static Keys[] GetPressedKeys() => currentState.GetPressedKeys();

    public static void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }
}