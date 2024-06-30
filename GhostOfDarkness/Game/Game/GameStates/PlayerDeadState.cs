﻿using game;
using Game.Controllers.Buttons;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;

namespace Game.Game.GameStates;

internal class PlayerDeadState : GameState
{
    public PlayerDeadState(IGameStateSwitcher stateSwitcher, IButtonFactory buttonFactory) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.UIBackground));

        var position = new Vector2(736, 430);
        var restart = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, position, "Restart");
        restart.OnClicked += Restart;
        Components.Add(restart);

        position.Y += 140;
        var mainMenu = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;
        Components.Add(mainMenu);
    }

    public override void Exit()
    {
        Switcher.SwitchState<MainMenuState>();
    }

    public override void Restart()
    {
        Switcher.StartGame();
        Switcher.SwitchState<PlayState>();
    }

    public override void Start(GameState previousState)
    {
    }

    public override void Stop()
    {
    }
}