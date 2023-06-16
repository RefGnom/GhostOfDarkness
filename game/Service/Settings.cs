using Microsoft.Xna.Framework.Input;

namespace game;

internal static class Settings
{
    public static bool ShowHitboxes { get; set; }
    public static bool ShowFPS { get; set; }
    public static float MusicVolumeStep { get; set; } = 0.01f;

    #region Player controls
    public static Keys Up => Keys.W;
    public static Keys Down => Keys.S;
    public static Keys Left => Keys.A;
    public static Keys Right => Keys.D;
    #endregion

    #region Game controls
    public static Keys OpenMenu => Keys.Escape;
    public static Keys OnFullScreen => Keys.F12;
    public static Keys TurnUpMusicVolume => Keys.OemPlus;
    public static Keys TurnDownMusicVolume => Keys.OemMinus;
    #endregion

    #region Debug mod
    public static Keys ShowOrHideHitboxes => Keys.B;
    public static Keys SwitchCameraFollow => Keys.F1;
    public static Keys SwitchPlayerCollision => Keys.F2;
    public static Keys ShowOrHideQuadTree => Keys.F3;
    public static Keys ShowOrHideFps => Keys.F4;
    #endregion
}