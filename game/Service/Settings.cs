using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game;

internal static class Settings
{
    private static GraphicsDeviceManager gameGraphics;

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
    #endregion

    #region Debug mod
    public static Keys ShowOrHideHitboxes => Keys.B;
    public static Keys SwitchCameraFollow => Keys.F1;
    public static Keys SwitchPlayerCollision => Keys.F2;
    public static Keys ShowOrHideQuadTree => Keys.F3;
    public static Keys ShowOrHideFps => Keys.F4;
    #endregion

    #region Graphics
    public static void SetGraphics(GraphicsDeviceManager graphics)
    {
        gameGraphics = graphics;
    }

    public static void SetSizeScreen(int width, int height)
    {
        gameGraphics.PreferredBackBufferWidth = width;
        gameGraphics.PreferredBackBufferHeight = height;
        gameGraphics.ApplyChanges();
    }

    public static void SetDisplayMode(bool isFullScreen)
    {
        gameGraphics.IsFullScreen = isFullScreen;
        gameGraphics.ApplyChanges();
    }
    #endregion
}