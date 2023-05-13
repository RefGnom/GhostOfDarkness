using Microsoft.Xna.Framework.Input;

namespace game;

internal static class Settings
{
    public static bool ShowHitboxes;

    #region controls
    public static Keys Up = Keys.W;
    public static Keys Down = Keys.S;
    public static Keys Left = Keys.A;
    public static Keys Right = Keys.D;
    #endregion

    public static Keys OpenMenu = Keys.Escape;
    public static Keys SwitchScreen = Keys.F;
    public static Keys ShowOrHideHitboxes = Keys.B;
    public static Keys SwitchCameraFollow = Keys.F1;
}