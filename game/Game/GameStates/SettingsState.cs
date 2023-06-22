﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game;

internal class SettingsState : GameState
{
    private Sprite[] options;

    public SettingsState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Exit()
    {
        switcher.SwitchState(previousState);
    }

    public override void Save()
    {
        Saved = true;
        switcher.SwitchState(previousState);
    }

    public override void Start(GameState previousState)
    {
        var background = new Sprite(Textures.Background, Vector2.Zero, Layers.Background);

        var position = new Vector2(1920 / 2 - Textures.SettingsString.Width / 2, 30);
        position = new Vector2(30, 30);
        var height = 60;
        var string1 = CreateString(position, "Full screen", Settings.OnFullScreen);
        position.Y += height; 
        var string2 = CreateString(position, "Menu", Settings.OpenMenu);
        position.Y += height;
        var string3 = CreateString(position, "Up", Settings.Up);
        position.Y += height;
        var string4 = CreateString(position, "Down", Settings.Down);
        position.Y += height;
        var string5 = CreateString(position, "Left", Settings.Left);
        position.Y += height;
        var string6 = CreateString(position, "Right", Settings.Right);
        position.Y += height;
        var string7 = CreateString(position, "Turn up music volume", Settings.TurnUpMusicVolume);
        position.Y += height;
        var string8 = CreateString(position, "Turn down music volume", Settings.TurnDownMusicVolume);

        options = new Sprite[]
        {
            background,
            string1,
            string2,
            string3,
            string4,
            string5,
            string6,
            string7,
            string8,
        };

        var save = new Button(Textures.ButtonBackground, new Vector2(934, 960), "Save");
        save.OnClicked += Save;

        var exit = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Exit");
        exit.OnClicked += Exit;

        buttons = new()
        {
            save,
            exit
        };

        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    private static Sprite CreateString(Vector2 position, string name, Keys key)
    {
        var widthName = 500;
        var line = new Sprite(Textures.SettingsString, position, Layers.UIBackground);
        line.AddText(new Text(new Rectangle(position.ToPoint(), new Point(widthName, Textures.SettingsString.Height)), name, Align.Left, 10, Fonts.Buttons));
        line.AddText(new Text(new Rectangle((int)position.X + widthName, (int)position.Y, 250, Textures.SettingsString.Height), key.ToString(), Align.Center, 0, Fonts.Buttons));
        return line;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].Draw(spriteBatch, scale);
        }
        base.Draw(spriteBatch, scale);
    }
}