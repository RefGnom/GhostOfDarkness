﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PlayerDeadState : GameState
{
    private Sprite background;

    public PlayerDeadState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void NewGame()
    {
    }

    public override void OpenSettings()
    {
    }

    public override void Play()
    {
    }

    public override void Dead()
    {
    }

    public override void Restart()
    {
        switcher.SwitchState<PlayState>();
    }

    public override void Save()
    {
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(TexturesManager.PauseBackground, new Vector2(564, 312));

        var position = new Vector2(736, 430);
        var mainMenu = new Button(TexturesManager.ButtonBackground, position, "In main menu");
        mainMenu.OnClicked += Exit;
        position.Y += 140;
        var restart = new Button(TexturesManager.ButtonBackground, position, "Restart");
        restart.OnClicked += Restart;

        buttons = new()
        {
            mainMenu,
            restart
        };

        GameManager.Instance.Drawer.RegisterUI(background);
        RegisterButtons();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        GameManager.Instance.Drawer.UnregisterUI(background);
        UnregisterButtons();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Update(float deltaTime)
    {
    }
}