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
using game.Input;
using game.Controllers;

namespace game.View;

internal class Game1 : Game, IPauseHandler
{
    private Dictionary<Keys, Action<float>> actions;

    private GameModel model;
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
        model = new(new Vector2(WindowWidth / 2, WindowHeight / 2), 1920, 1080);
        controller = new(graphics);

        controller.SetSizeScreen(1280, 720);

        GameManager.Instance.PauseManager.RegisterHandler(this);
        RegisterAllKeys();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardController.Update();
        MouseController.Update();
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
        spriteBatch.Begin(transformMatrix: Camera.Transform);
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