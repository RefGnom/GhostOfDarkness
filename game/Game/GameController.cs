using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

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

    public void Update(float deltaTime)
    {
        KeyboardController.Update();
        MouseController.Update();

        if (GameIsExit)
            view.CloseGame();

        if (model.Started && model.Player.IsDead)
            Dead();

        if (KeyboardController.IsSingleKeyDown(Keys.Escape))
            Back();

        if (KeyboardController.IsSingleKeyDown(Keys.Enter))
        {
            Confirm();
            Save();
        }

        if (KeyboardController.IsSingleKeyDown(Settings.OnFullScreen))
        {
            view.SwitchScreenState();
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

        if (KeyboardController.IsKeyDown(Settings.TurnDownMusicVolume, false))
            MediaPlayer.Volume -= Settings.MusicVolumeStep;
        if (KeyboardController.IsKeyDown(Settings.TurnUpMusicVolume, false))
            MediaPlayer.Volume += Settings.MusicVolumeStep;

        if (IsPlay)
            UpdateModel(deltaTime);

        IsPaused = !IsPlay;
    }

    public override void StartGame()
    {
        model.Delete();
        model.Start();
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