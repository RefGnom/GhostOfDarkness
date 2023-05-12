using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game.Input;

internal static class MyMouse
{
    private static MouseState currentState;
    private static MouseState previousState;

    public static Vector2 Position => currentState.Position.ToVector2();

    public static bool LeftButtomClicked()
    {
        return currentState.LeftButton == ButtonState.Pressed
            && previousState.LeftButton == ButtonState.Released;
    }

    public static bool RightButtomClicked()
    {
        return currentState.RightButton == ButtonState.Pressed
            && previousState.RightButton == ButtonState.Released;
    }

    public static int ScrollValue()
    {
        return (currentState.ScrollWheelValue - previousState.ScrollWheelValue) / 120;
    }

    public static void Update()
    {
        previousState = currentState;
        currentState = Mouse.GetState();
    }
}