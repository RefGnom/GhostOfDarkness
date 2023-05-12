using game.Input;
using game.Managers;
using game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game.Controllers;

internal class Controller
{
    private GraphicsDeviceManager graphics;

    private PauseManager PauseManager => GameManager.Instance.PauseManager;

    public Controller(GraphicsDeviceManager graphics)
    {
        this.graphics = graphics;
    }

    public void Update()
    {
        if (KeyboardController.IsSingleKeyDown(Settings.ChangeScreen))
        {
            if (graphics.IsFullScreen)
                SetSizeScreen(1280, 720);
            else
                OnFullScreen();
        }

        if (KeyboardController.IsSingleKeyDown(Settings.OpenMenu))
            PauseManager.SetPaused(!PauseManager.IsPaused);

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideHitboxes))
            Settings.ShowHitboxes = !Settings.ShowHitboxes;
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