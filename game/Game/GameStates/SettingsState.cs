using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace game;

internal class SettingsState : GameState
{
    public SettingsState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var position = new Vector2(1920 / 2 - Textures.SettingsString.Width / 2, 30);
        position = new Vector2(30, 30);
        var height = 60;
        drawables.Add(CreateString(position, "Full screen", Settings.OnFullScreen));

        position.Y += height;
        drawables.Add(CreateString(position, "Menu", Settings.OpenMenu));

        position.Y += height;
        drawables.Add(CreateString(position, "Up", Settings.Up));

        position.Y += height;
        drawables.Add(CreateString(position, "Down", Settings.Down));

        position.Y += height;
        drawables.Add(CreateString(position, "Left", Settings.Left));

        position.Y += height;
        drawables.Add(CreateString(position, "Right", Settings.Right));

        var textWidth = 280;
        position = new Vector2(1200, 40);
        var musicVolume = new ProgressBar(0, 1, position, 6);
        musicVolume.ValueOnChanged += (value) => MediaPlayer.Volume = value;
        musicVolume.SetValue(0.3f);
        var musicVolumeText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 16, textWidth, height), "Громкость музыки", Align.Left, 0, Fonts.Common16);
        components.Add(musicVolume);
        drawables.Add(musicVolumeText);

        position.Y += height;
        var soundVolume = new ProgressBar(0, 1, position, 6);
        soundVolume.ValueOnChanged += (value) => SoundEffect.MasterVolume = value;
        soundVolume.SetValue(0.3f);
        var soundVolumeText = new Text(new Rectangle((int)position.X - textWidth, (int)position.Y - 16, textWidth, height), "Громкость звуков", Align.Left, 0, Fonts.Common16);
        components.Add(soundVolume);
        drawables.Add(soundVolumeText);

        var save = new Button(Textures.ButtonBackground, new Vector2(934, 960), "Save");
        save.OnClicked += Save;
        components.Add(save);

        var exit = new Button(Textures.ButtonBackground, new Vector2(1432, 960), "Exit");
        exit.OnClicked += Exit;
        components.Add(exit);
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
    }

    public override void Stop()
    {
    }

    private static Sprite CreateString(Vector2 position, string name, Keys key)
    {
        var widthName = 500;
        var line = new Sprite(Textures.SettingsString, position, Layers.UIBackground);
        line.AddText(new Text(new Rectangle(position.ToPoint(), new Point(widthName, Textures.SettingsString.Height)), name, Align.Left, 10, Fonts.Buttons));
        line.AddText(new Text(new Rectangle((int)position.X + widthName, (int)position.Y, 250, Textures.SettingsString.Height), key.ToString(), Align.Center, 0, Fonts.Buttons));
        return line;
    }
}