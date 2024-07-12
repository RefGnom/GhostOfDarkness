using Core.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using IUpdateable = Game.Interfaces.IUpdateable;

namespace Game.Controllers.InputServices;


[DiUsage]
public interface IKeyboardService : IUpdateable
{
    void SetGameWindow(GameWindow gameWindow);
    GameWindow GetGameWindow();
    bool IsKeyDown(Keys key);
    bool IsKeysDown(params Keys[] keys);
    bool IsSingleKeyDown(Keys key);
    bool IsSingleKeyUp(Keys key);
    Keys[] GetPressedKeys();
}