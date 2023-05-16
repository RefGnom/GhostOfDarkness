using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Controller
{
    private GraphicsDeviceManager graphics;

    private Camera Camera => GameManager.Instance.Camera;

    public Controller(GraphicsDeviceManager graphics)
    {
        this.graphics = graphics;
    }

    public void Update()
    {
        if (KeyboardController.IsSingleKeyDown(Settings.SwitchScreen))
        {
            if (graphics.IsFullScreen)
                SetSizeScreen(1280, 720);
            else
                OnFullScreen();
        }

        //if (KeyboardController.IsSingleKeyDown(Settings.OpenMenu))
            //PauseManager.SetPaused(!PauseManager.IsPaused);

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideHitboxes))
            Settings.ShowHitboxes = !Settings.ShowHitboxes;

        if (KeyboardController.IsSingleKeyDown(Settings.SwitchCameraFollow))
            Camera.FollowPlayer = !Camera.FollowPlayer;
    }

    public void SetSizeScreen(int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.IsFullScreen = false;
        graphics.ApplyChanges();
    }

    public void OnFullScreen()
    {
        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graphics.IsFullScreen = true;
        graphics.ApplyChanges();
    }
}