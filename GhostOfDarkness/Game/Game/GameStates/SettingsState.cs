using System;
using Game.ContentLoaders;
using Game.Controllers.Buttons;
using Game.Controllers.Switcher;
using Game.Enums;
using Game.Service;
using Game.View;
using Game.View.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game.Game.GameStates;

internal class SettingsState : GameState
{
    private Action musicVolumeAction;
    private Action soundsVolumeAction;
    private Action displayModeAction;
    private Action resolutionAction;

    public SettingsState(IButtonFactory buttonFactory)
    {
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var position = new Vector2(1920 / 2 - Textures.SettingsString.Width / 2, 30);
        position = new Vector2(30, 30);
        var height = 60;
        Drawables.Add(CreateString(position, "Menu", Settings.OpenMenu));

        position.Y += height;
        Drawables.Add(CreateString(position, "Up", Settings.Up));

        position.Y += height;
        Drawables.Add(CreateString(position, "Down", Settings.Down));

        position.Y += height;
        Drawables.Add(CreateString(position, "Left", Settings.Left));

        position.Y += height;
        Drawables.Add(CreateString(position, "Right", Settings.Right));

        var textWidth = 280;
        position = new Vector2(1200, 30);
        var displayMode = new Switcher(position)
        {
            { "Оконный режим", () => displayModeAction = () => Settings.SetDisplayMode(false) },
            { "На весь экран", () => displayModeAction = () => Settings.SetDisplayMode(true) },
        };
        displayMode.Start();
        var displayModeText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 12, textWidth, height), "Режим отображения", Fonts.Common16, Align.Left);
        Components.Add(displayMode);
        Drawables.Add(displayModeText);

        position.Y += height;
        var resolution = new Switcher(position)
        {
            { "1280x720", () => resolutionAction = () => Settings.SetSizeScreen(1280, 720) },
            { "1920x1080", () => resolutionAction = () => Settings.SetSizeScreen(1920, 1080) },
            { "2560x1440", () => resolutionAction = () => Settings.SetSizeScreen(2560, 1440) },
            { "3840x2160", () => resolutionAction = () => Settings.SetSizeScreen(3840, 2160) },
        };
        resolution.Start();
        var resolutionText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 12, textWidth, height), "Разрешение", Fonts.Common16, Align.Left);
        Components.Add(resolution);
        Drawables.Add(resolutionText);

        position = new Vector2(1200, 300);
        var musicVolume = new ProgressBar(0, 1, position, 6);
        musicVolume.ValueOnChanged += (value) => musicVolumeAction = () => MediaPlayer.Volume = value;
        musicVolume.SetValue(0.3f);
        var musicVolumeText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 16, textWidth, height), "Громкость музыки", Fonts.Common16, Align.Left);
        Components.Add(musicVolume);
        Drawables.Add(musicVolumeText);

        position.Y += height;
        var soundVolume = new ProgressBar(0, 1, position, 6);
        soundVolume.ValueOnChanged += (value) => soundsVolumeAction = () => SoundEffect.MasterVolume = value;
        soundVolume.SetValue(0.3f);
        var soundVolumeText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 16, textWidth, height), "Громкость звуков", Fonts.Common16, Align.Left);
        Components.Add(soundVolume);
        Drawables.Add(soundVolumeText);

        var save = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(934, 960), "Save");
        save.OnClicked += Save;
        Components.Add(save);

        var exit = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, new Vector2(1432, 960), "Exit");
        exit.OnClicked += Exit;
        Components.Add(exit);

        SaveSettins();
    }

    public override void Back()
    {
        Switcher.SwitchState(PreviousState);
    }

    public override void Exit()
    {
        Switcher.SwitchState(PreviousState);
    }

    public override void Save()
    {
        SaveSettins();
    }

    private void SaveSettins()
    {
        musicVolumeAction?.Invoke();
        musicVolumeAction = null;
        soundsVolumeAction?.Invoke();
        soundsVolumeAction = null;
        resolutionAction?.Invoke();
        resolutionAction = null;
        displayModeAction?.Invoke();
        displayModeAction = null;
    }

    private static Sprite CreateString(Vector2 position, string name, Keys key)
    {
        var widthName = 500;
        var line = new Sprite(Textures.SettingsString, position, Layers.UiBackground);
        line.AddText(new Text(new Rectangle(position.ToPoint(), new Point(widthName, Textures.SettingsString.Height)), name, Fonts.Buttons, Align.Left, 10));
        line.AddText(new Text(new Rectangle((int)position.X + widthName, (int)position.Y, 250, Textures.SettingsString.Height), key.ToString(), Fonts.Buttons));
        return line;
    }
}