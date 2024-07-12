using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game.Controllers.InputServices;

public class KeyboardService : IKeyboardService
{
    private GameWindow window;
    private KeyboardState currentState;
    private KeyboardState previousState;

    public void SetGameWindow(GameWindow gameWindow)
    {
        window = gameWindow;
    }

    public GameWindow GetGameWindow() => window;

    public bool IsKeyDown(Keys key) => currentState.IsKeyDown(key);

    public bool IsKeysDown(params Keys[] keys) => keys.All(IsKeyDown);

    public bool IsSingleKeyDown(Keys key) => currentState.IsKeyDown(key) && !previousState.IsKeyDown(key);

    public bool IsSingleKeyUp(Keys key) => currentState.IsKeyUp(key) && !previousState.IsKeyUp(key);

    public Keys[] GetPressedKeys() => currentState.GetPressedKeys();

    public void Update(float deltaTime)
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }
}