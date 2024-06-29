using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers;

internal static class KeyboardController
{
    private static KeyboardState currentState;
    private static KeyboardState previousState;

    public static GameWindow GameWindow { get; set; }

    public static bool IsKeyDown(Keys key, bool single = true) => currentState.IsKeyDown(key) && (!previousState.IsKeyDown(key) || !single);

    public static bool IsCombinationKeysDown(params Keys[] keys)
    {
        return keys.All(key => IsKeyDown(key, false));
    }

    public static bool IsSingleKeyDown(Keys key) => currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);

    public static bool IsSingleKeyUp(Keys key) => currentState.IsKeyUp(key) && !previousState.IsKeyUp(key);

    public static Keys[] GetPressedKeys() => currentState.GetPressedKeys();

    public static void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }
}