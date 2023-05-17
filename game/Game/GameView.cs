﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class GameView : Game
{
    private readonly float maxWindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

    private GameModel model;
    private GameController controller;

    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public int WindowWidth => graphics.PreferredBackBufferWidth;
    public int WindowHeight => graphics.PreferredBackBufferHeight;
    public float Scale => WindowWidth / maxWindowWidth;

    private static Camera Camera => GameManager.Instance.Camera;

    public GameView()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        LoadContent();
        model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), 1920, 1080);
        controller = new(model, this);
        SetSizeScreen(1280, 720);
        Debug.Initialize(WindowHeight);
        KeyboardController.GameWindow = Window;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        TexturesManager.Load(Content);
        FontsManager.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        controller.Update(deltaTime);

        Camera.Follow(model.Player.Position, WindowWidth, WindowHeight, deltaTime);
        Camera.ChangeScale(MouseController.ScrollValue());

        Debug.Update(WindowHeight);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        DrawLocation();
        DrawHUD();
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
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawUI(spriteBatch, Scale);
        spriteBatch.End();
    }

    private void DrawHUD()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawHUD(spriteBatch, Scale);
        spriteBatch.End();
    }
    #endregion
}