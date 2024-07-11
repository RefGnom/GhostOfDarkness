using System;
using Game.ContentLoaders;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Managers;
using Game.Service;
using Game.View;
using Game.View.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Game.Game;

internal class GameView : Microsoft.Xna.Framework.Game
{
    private GameModel model;
    private GameController controller;
    private Fps fps;

    private readonly GraphicsDeviceManager graphics;
    private ISpriteBatch spriteBatch;

    public static int WindowWidth { get; private set; }
    public static int WindowHeight { get; private set; }
    public static Vector2 Center => new Vector2(WindowWidth / 2, WindowHeight / 2);
    public static float Scale => WindowWidth / 1920f;

    private static Camera Camera => GameManager.Instance.Camera;

    public GameView()
    {
        graphics = new GraphicsDeviceManager(this);
        Settings.SetGraphics(graphics);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
        IsFixedTimeStep = false;
        graphics.SynchronizeWithVerticalRetrace = true;
        graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        base.Initialize();

        model = new GameModel();
        controller = new GameController(model, this, Input.MouseService, Input.KeyboardService);
        fps = new Fps(0.3f);
        Debug.Initialize(WindowHeight);
        Input.KeyboardService.SetGameWindow(Window);
        Input.MouseService.SetCamera(Camera);
        MediaPlayer.Volume = 0.3f;

        box = new ScrollBar(new Vector2(800, 100), Textures.SavesWindow, Textures.Wall, new Point(0, 0));
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatchWrapper(GraphicsDevice);
        Textures.Load(Content);
        Fonts.Load(Content);
        Sounds.Load(Content);
    }

    private ScrollBar box;
    protected override void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        WindowWidth = graphics.PreferredBackBufferWidth;
        WindowHeight = graphics.PreferredBackBufferHeight;
        GameManager.Instance.Update();
        fps.Update(gameTime);
        SongsManager.Update();
        controller.Update(deltaTime);

        box.Update(deltaTime);

        if (model.Started)
        {
            Camera.Follow(model.Player.Position, WindowWidth, WindowHeight, deltaTime);
            Camera.ChangeScale(Input.MouseService.ScrollValue());
        }

        Debug.Update(WindowHeight);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(40, 32, 27, 255));

        if (model.Started)
        {
            DrawLocation();
            DrawHud();
        }
        DrawUi();

        base.Draw(gameTime);
    }

    public void CloseGame() => Exit();

    #region Draw
    private void DrawLocation()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: Camera.Transform);
        GameManager.Instance.Drawer.Draw(spriteBatch, Scale);
        spriteBatch.End();
    }

    private void DrawUi()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        controller.Draw(spriteBatch, Scale);
        Debug.DrawMessages(spriteBatch);
        spriteBatch.Draw(Textures.SavesWindow, new Vector2(800, 100) * Scale, Scale, Layers.Ui);
        box.Draw(spriteBatch, Scale);
        GameManager.Instance.Drawer.DrawUi(spriteBatch, Scale);
        spriteBatch.End();
    }

    private void DrawHud()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        HintManager.Draw(spriteBatch, Scale);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawHud(spriteBatch, Scale);
        spriteBatch.End();
    }
    #endregion
}