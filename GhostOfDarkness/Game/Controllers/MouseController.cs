using System;
using Game.Managers;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers;

internal static class MouseController
{
    private static MouseState currentState;
    private static MouseState previousState;

    public static Vector2 WindowPosition => currentState.Position.ToVector2();
    public static Vector2 WorldPosition => Camera.ScreenToWorld(WindowPosition);
    private static Camera Camera => GameManager.Instance.Camera;

    public static event Action LeftButtonOnClicked;

    public static bool LeftButtonPressed() => currentState.LeftButton == ButtonState.Pressed;

    public static bool LeftButtonClicked() => currentState.LeftButton == ButtonState.Pressed
                                              && previousState.LeftButton == ButtonState.Released;

    public static bool LeftButtonReleased() => currentState.LeftButton == ButtonState.Released
                                                && previousState.LeftButton == ButtonState.Pressed;

    public static bool RightButtonClicked() => currentState.RightButton == ButtonState.Pressed
                                               && previousState.RightButton == ButtonState.Released;

    public static int ScrollValue() => (currentState.ScrollWheelValue - previousState.ScrollWheelValue) / 120;

    public static void Update()
    {
        if (LeftButtonClicked())
        {
            LeftButtonOnClicked?.Invoke();
        }

        previousState = currentState;
        currentState = Mouse.GetState();
    }
}