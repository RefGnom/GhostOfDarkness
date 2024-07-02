using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Game.Controllers;
using Game.Game;
using Game.Game.GameStates;
using Game.Structures;

namespace game;

internal class GameController : GameStatesController
{
    private readonly Dictionary<Keys, Action> actions;

    private readonly GameModel model;
    private readonly GameView view;

    private static Camera Camera => GameManager.Instance.Camera;
    public static bool IsPaused { get; private set; }

    public GameController(GameModel model, GameView view)
    {
        actions = new();
        this.model = model;
        this.view = view;
        RegisterKeys();
    }

    public override void Update(float deltaTime)
    {
        KeyboardController.Update();
        MouseController.Update();

        if (GameIsExit)
            view.CloseGame();

        if (model.Started && model.Player.IsDead)
            Dead();

        if (KeyboardController.IsSingleKeyDown(Keys.Escape))
            Back();

        //var keys = KeyboardController.GetPressedKeys();
        //foreach (var key in keys)
        //{
        //    Debug.Log($"{key} {keys.Length}");
        //}

        if (KeyboardController.IsSingleKeyDown(Keys.Enter))
        {
            Confirm();
            Save();
        }

        if (KeyboardController.IsSingleKeyDown(Settings.SwitchPlayerCollision))
        {
            if (model.Started)
                model.Player.IsCollide = !model.Player.IsCollide;
        }

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideQuadTree))
        {
            QuadTree.Show = !QuadTree.Show;
        }

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideFps))
        {
            Settings.ShowFPS = !Settings.ShowFPS;
        }

        if (IsPlay)
            UpdateModel(deltaTime);

        IsPaused = !IsPlay;
        base.Update(deltaTime);
    }

    public override void StartGame()
    {
        model.Delete();
        model.Start();
        GameManager.Instance.DialogManager.Enable(Story.GetPrologue());
    }

    private void UpdateModel(float deltaTime)
    {
        model.Update(deltaTime);

        if (KeyboardController.IsSingleKeyDown(Settings.ShowOrHideHitboxes))
            Settings.ShowHitboxes = !Settings.ShowHitboxes;

        if (KeyboardController.IsSingleKeyDown(Settings.SwitchCameraFollow))
            Camera.FollowPlayer = !Camera.FollowPlayer;

        HandleKeys(KeyboardController.GetPressedKeys());
    }

    private void HandleKeys(Keys[] keys)
    {
        foreach (var key in keys)
        {
            if (actions.ContainsKey(key))
                actions[key]();
        }
    }

    private void RegisterKeys()
    {
        actions[Settings.Up] = () => model.Player.DeltaY -= 1;
        actions[Settings.Down] = () => model.Player.DeltaY += 1;
        actions[Settings.Left] = () => model.Player.DeltaX -= 1;
        actions[Settings.Right] = () => model.Player.DeltaX += 1;
    }
}