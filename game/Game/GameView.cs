using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;

namespace game;

internal class GameView : Game
{
    private readonly float maxWindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    private GameModel model;
    private GameController controller;
    private Fps fps;

    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public static int WindowWidth { get; private set; }
    public static int WindowHeight { get; private set; }
    public static Vector2 Center => new(WindowWidth / 2, WindowHeight / 2);
    public float Scale => WindowWidth / maxWindowWidth;

    private static Camera Camera => GameManager.Instance.Camera;

    public GameView()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
        IsFixedTimeStep = false;
        graphics.SynchronizeWithVerticalRetrace = true;
        graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        LoadContent();
        SetSizeScreen(1280, 720);
        model = new();
        controller = new(model, this);
        fps = new(0.3f);
        Debug.Initialize(WindowHeight);
        KeyboardController.GameWindow = Window;
        MediaPlayer.Volume = 0.3f;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Textures.Load(Content);
        Fonts.Load(Content);
        Sounds.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        WindowWidth = graphics.PreferredBackBufferWidth;
        WindowHeight = graphics.PreferredBackBufferHeight;
        GameManager.Instance.Update();
        fps.Update(gameTime);
        SongsManager.Update();
        controller.Update(deltaTime);

        if (model.Started)
        {
            Camera.Follow(model.Player.Position, WindowWidth, WindowHeight, deltaTime);
            Camera.ChangeScale(MouseController.ScrollValue());
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
            DrawHUD();
        }
        DrawUI();

        base.Draw(gameTime);
    }

    public void CloseGame() => Exit();

    #region ScreenSize
    public void SwitchScreenState()
    {
        if (graphics.IsFullScreen)
            SetSizeScreen(1280, 720);
        else
            OnFullScreen();
    }

    private void SetSizeScreen(int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.IsFullScreen = false;
        graphics.ApplyChanges();
    }

    private void OnFullScreen()
    {
        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graphics.IsFullScreen = true;
        graphics.ApplyChanges();
    }
    #endregion

    #region Draw
    private void DrawLocation()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: Camera.Transform);
        GameManager.Instance.Drawer.Draw(spriteBatch, Scale);
        spriteBatch.End();
    }

    private void DrawUI()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        controller.Draw(spriteBatch, Scale);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawUI(spriteBatch, Scale);
        spriteBatch.End();
    }

    private void DrawHUD()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        HintManager.Draw(spriteBatch, Scale);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawHUD(spriteBatch, Scale);
        spriteBatch.End();
    }
    #endregion
}