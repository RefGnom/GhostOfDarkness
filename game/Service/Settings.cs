using Microsoft.Xna.Framework.Input;

namespace game.Service;

internal static class Settings
{
    #region controls
    public static Keys MoveForward = Keys.W;
    public static Keys MoveBack = Keys.S;
    public static Keys MoveLeft = Keys.A;
    public static Keys MoveRight = Keys.D;
    #endregion

    public static Keys OpenMenu = Keys.Escape;
    public static Keys ChangeScreen = Keys.F;
}