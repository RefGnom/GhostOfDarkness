using Core.DependencyInjection;
using Game.View;
using Microsoft.Xna.Framework;
using IUpdateable = Game.Interfaces.IUpdateable;

namespace Game.Controllers.InputServices;

[DiUsage]
public interface IMouseService : IUpdateable
{
    void SetCamera(Camera camera);
    Vector2 GetWindowPosition();
    Vector2 GetWorldPosition();
    bool LeftButtonClicked();
    bool LeftButtonReleased();
    bool LeftButtonPressed();
    bool RightButtonClicked();
    bool RightButtonReleased();
    bool RightButtonPressed();
    int GetScrollValue();
}