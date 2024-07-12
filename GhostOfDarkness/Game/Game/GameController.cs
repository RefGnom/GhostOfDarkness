using System;
using System.Collections.Generic;
using Game.Controllers.InputServices;
using Game.Game.GameStates;
using Game.Managers;
using Game.Service;
using Game.Structures;
using Game.View;
using Microsoft.Xna.Framework.Input;

namespace Game.Game;

internal class GameController : GameStatesController
{
    private readonly Dictionary<Keys, Action> actions;

    private readonly GameModel model;
    private readonly GameView view;
    private readonly IMouseService mouseService;
    private readonly IKeyboardService keyboardService;

    private static Camera Camera => GameManager.Instance.Camera;
    public static bool IsPaused { get; private set; }

    public GameController(GameModel model, GameView view, IMouseService mouseService, IKeyboardService keyboardService)
    {
        actions = new Dictionary<Keys, Action>();
        this.model = model;
        this.view = view;
        this.mouseService = mouseService;
        this.keyboardService = keyboardService;
        RegisterKeys();
    }

    public override void Update(float deltaTime)
    {
        keyboardService.Update(deltaTime);
        mouseService.Update(deltaTime);

        if (GameIsExit)
        {
            view.CloseGame();
        }

        if (model.Started && model.Player.IsDead)
        {
            Dead();
        }

        if (keyboardService.IsSingleKeyDown(Keys.Escape))
        {
            Back();
        }

        //var keys = KeyboardController.GetPressedKeys();
        //foreach (var key in keys)
        //{
        //    Debug.Log($"{key} {keys.Length}");
        //}

        if (keyboardService.IsSingleKeyDown(Keys.Enter))
        {
            Confirm();
            Save();
        }

        if (keyboardService.IsSingleKeyDown(Settings.SwitchPlayerCollision) && model.Started)
        {
            model.Player.IsCollide = !model.Player.IsCollide;
        }

        if (keyboardService.IsSingleKeyDown(Settings.ShowOrHideQuadTree))
        {
            QuadTree.Show = !QuadTree.Show;
        }

        if (keyboardService.IsSingleKeyDown(Settings.ShowOrHideFps))
        {
            Settings.ShowFps = !Settings.ShowFps;
        }

        if (IsPlay)
        {
            UpdateModel(deltaTime);
        }

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

        if (keyboardService.IsSingleKeyDown(Settings.ShowOrHideHitboxes))
        {
            Settings.ShowHitboxes = !Settings.ShowHitboxes;
        }

        if (keyboardService.IsSingleKeyDown(Settings.SwitchCameraFollow))
        {
            Camera.FollowPlayer = !Camera.FollowPlayer;
        }

        HandleKeys(keyboardService.GetPressedKeys());
    }

    private void HandleKeys(Keys[] keys)
    {
        foreach (var key in keys)
        {
            if (actions.TryGetValue(key, out var action))
            {
                action();
            }
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