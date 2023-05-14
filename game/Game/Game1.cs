using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace game;

internal class Game1 : Game, IPauseHandler
{
    private Dictionary<Keys, Action<float>> actions;

    private GameModel model;
    private GameStatesController view;
    private Controller controller;
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public int WindowWidth => graphics.PreferredBackBufferWidth;
    public int WindowHeight => graphics.PreferredBackBufferHeight;

    private PauseManager PauseManager => GameManager.Instance.PauseManager;
    private Camera Camera => GameManager.Instance.Camera;

    public Game1()
    {
        GameManager.Instance.Game = this;
        actions = new();
        graphics = new GraphicsDeviceManager(this);
        controller = new(graphics);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Текстуры должны подгружаться раньше, чем создастся модель уровня
        TexturesManager.Load(Content);
        // ???
        model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), 1920, 1080);
        view = new();
        controller.SetSizeScreen(1280, 720);
        GameManager.Instance.PauseManager.RegisterHandler(this);
        RegisterAllKeys();
        Debug.Initialize(WindowHeight);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        FontsManager.Load(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardController.Update();
        MouseController.Update();
        Debug.Update(WindowHeight);
        controller.Update();

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        HandleKeys(KeyboardController.GetPressedKeys(), deltaTime);

        model.Update(deltaTime);
        Camera.Follow(model.Player.Position, WindowWidth, WindowHeight, deltaTime);
        Camera.ChangeScale(MouseController.ScrollValue());

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (PauseManager.IsPaused)
            return;
        GraphicsDevice.Clear(Color.CornflowerBlue);
        DrawUI();
        Draw();
        DrawHUD();
        base.Draw(gameTime);
    }

    private void Draw()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, transformMatrix: Camera.Transform);
        GameManager.Instance.Drawer.Draw(spriteBatch);
        spriteBatch.End();
    }

    private void DrawUI()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawUI(spriteBatch);
        spriteBatch.End();
    }

    private void DrawHUD()
    {
        spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);
        Debug.DrawMessages(spriteBatch);
        GameManager.Instance.Drawer.DrawHUD(spriteBatch);
        spriteBatch.End();
    }

    public void HandleKeys(Keys[] keys, float deltaTime)
    {
        foreach (var key in keys)
        {
            if (actions.ContainsKey(key))
                actions[key](deltaTime);
        }
    }

    public void RegisterAllKeys()
    {
        actions[Settings.Up] = deltaTime => model.Player.EnableDirections[Directions.Up] = -1;
        actions[Settings.Down] = deltaTime => model.Player.EnableDirections[Directions.Down] = 1;
        actions[Settings.Left] = deltaTime => model.Player.EnableDirections[Directions.Left] = -1;
        actions[Settings.Right] = deltaTime => model.Player.EnableDirections[Directions.Right] = 1;
    }

    public void SetPaused(bool isPaused)
    {
        if (isPaused)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(TexturesManager.PauseMenu, Vector2.Zero, null, Color.White, 0, Vector2.One, 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}