using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers.InputServices;

public class MouseService : IMouseService
{
    private MouseState currentState;
    private MouseState previousState;
    private Camera camera;

    // ReSharper disable once ParameterHidesMember
    public void SetCamera(Camera camera)
    {
        this.camera = camera;
    }

    public Vector2 GetWindowPosition() => currentState.Position.ToVector2();

    public Vector2 GetWorldPosition() => camera.ScreenToWorld(GetWindowPosition());

    public bool LeftButtonClicked() => currentState.LeftButton == ButtonState.Pressed
                                       && previousState.LeftButton == ButtonState.Released;

    public bool LeftButtonReleased() => currentState.LeftButton == ButtonState.Released
                                        && previousState.LeftButton == ButtonState.Pressed;

    public bool LeftButtonPressed() => currentState.LeftButton == ButtonState.Pressed;

    public bool RightButtonClicked() => currentState.RightButton == ButtonState.Pressed
                                        && previousState.RightButton == ButtonState.Released;

    public bool RightButtonReleased() => currentState.RightButton == ButtonState.Released
                                         && previousState.RightButton == ButtonState.Pressed;

    public bool RightButtonPressed() => currentState.RightButton == ButtonState.Pressed;

    public int ScrollValue() => (currentState.ScrollWheelValue - previousState.ScrollWheelValue) / 120;

    public void Update(float deltaTime)
    {
        previousState = currentState;
        currentState = Mouse.GetState();
    }
}