using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using game.Managers;
using game.Interfaces;
using game.Enums;
using game.Model;
using game.Service;

namespace game.View;

internal class Game1 : Game, IPauseHandler
{
    private Dictionary<Keys, Action<float>> actions;

    private GameModel model;

    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public int WindowWidth => graphics.PreferredBackBufferWidth;
    public int WindowHeight => graphics.PreferredBackBufferHeight;

    private PauseManager PauseManager => GameManager.Instance.PauseManager;

    public Game1()
    {
        GameManager.Instance.Game = this;
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Текстуры должны подгружаться раньше, чем создастся модель уровня
        TexturesManager.Load(Content);
        // ???
        actions = new();
        model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), WindowWidth, WindowHeight);

        OnFullScreen();
        SetSizeScreen(1280, 720);

        GameManager.Instance.PauseManager.RegisterHandler(this);
        RegisterAllKeys();
        base.Initialize();
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

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            Exit();

        if (KeyboardManager.IsSingleDown(Settings.ChangeScreen))
        {
            if (graphics.IsFullScreen) SetSizeScreen(1280, 720);
            else OnFullScreen();
        }

        if (KeyboardManager.IsSingleDown(Keys.Escape))
            PauseManager.SetPaused(!PauseManager.IsPaused);

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();

        var mousePosition = mouseState.Position.ToVector2();
        var direction = mousePosition - model.Player.Position;
        direction.Normalize();

        var pressedKeys = keyboardState.GetPressedKeys();
        HandleKeys(pressedKeys, deltaTime);

        model.Update(deltaTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (PauseManager.IsPaused)
            return;

        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        GameManager.Instance.Drawer.Draw(spriteBatch);
        spriteBatch.End();
        base.Draw(gameTime);
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
        actions[Settings.MoveForward] = deltaTime => model.Player.EnableDirections[Directions.Up] = true;
        actions[Settings.MoveBack] = deltaTime => model.Player.EnableDirections[Directions.Down] = true;
        actions[Settings.MoveLeft] = deltaTime => model.Player.EnableDirections[Directions.Left] = true;
        actions[Settings.MoveRight] = deltaTime => model.Player.EnableDirections[Directions.Right] = true;
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