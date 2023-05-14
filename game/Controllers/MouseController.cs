using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace game;

internal static class MouseController
{
    private static MouseState currentState;
    private static MouseState previousState;

    public static Vector2 WindowPosition => currentState.Position.ToVector2();
    public static Vector2 WorldPosition => Camera.ScreenToWorld(WindowPosition);
    private static Camera Camera => GameManager.Instance.Camera;

    public static event Action LeftButtonOnClicked;

    public static bool LeftButtonClicked()
    {
        return currentState.LeftButton == ButtonState.Pressed
            && previousState.LeftButton == ButtonState.Released;
    }

    public static bool RightButtonClicked()
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
        if (LeftButtonClicked())
            LeftButtonOnClicked?.Invoke();
        previousState = currentState;
        currentState = Mouse.GetState();
    }
}