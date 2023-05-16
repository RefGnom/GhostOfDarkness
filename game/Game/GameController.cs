using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace game;

internal class GameController
{
    private readonly Dictionary<Keys, Action> actions;

    private readonly GraphicsDeviceManager graphics;

    private readonly GameModel model;

    private static Camera Camera => GameManager.Instance.Camera;

    public GameController(GraphicsDeviceManager graphics, GameModel model)
    {
        actions = new();
        this.graphics = graphics;
        this.model = model;
        RegisterKeys();
    }

    public void Update(float deltaTime)
    {
        if (KeyboardController.IsSingleKeyDown(Settings.SwitchScreen))
        {
            if (graphics.IsFullScreen)
                SetSizeScreen(1280, 720);
            else
                OnFullScreen();
        }

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideHitboxes))
            Settings.ShowHitboxes = !Settings.ShowHitboxes;

        if (KeyboardController.IsSingleKeyDown(Settings.SwitchCameraFollow))
            Camera.FollowPlayer = !Camera.FollowPlayer;

        HandleKeys(KeyboardController.GetPressedKeys());
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

    public void HandleKeys(Keys[] keys)
    {
        foreach (var key in keys)
        {
            if (actions.ContainsKey(key))
                actions[key]();
        }
    }

    public void RegisterKeys()
    {
        actions[Settings.Up] = () => model.Player.DeltaY -= 1;
        actions[Settings.Down] = () => model.Player.DeltaY += 1;
        actions[Settings.Left] = () => model.Player.DeltaX -= 1;
        actions[Settings.Right] = () => model.Player.DeltaX += 1;
    }
}