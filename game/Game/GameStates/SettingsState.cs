using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game;

internal class SettingsState : GameState
{
    private Sprite[] sprites;

    public SettingsState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Exit()
    {
        switcher.SwitchState(previousState);
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
        var string1 = CreateString(position, "Full screen", Settings.OnFullScreen);
        position.Y += 60; 
        var string2 = CreateString(position, "Menu", Settings.OpenMenu);
        position.Y += 60;
        var string3 = CreateString(position, "Up", Settings.Up);
        position.Y += 60;
        var string4 = CreateString(position, "Down", Settings.Down);
        position.Y += 60;
        var string5 = CreateString(position, "Left", Settings.Left);
        position.Y += 60;
        var string6 = CreateString(position, "Right", Settings.Right);
        position.Y += 60;
        var string7 = CreateString(position, "Turn up music volume", Settings.TurnUpMusicVolume);
        position.Y += 60;
        var string8 = CreateString(position, "Turn down music volume", Settings.TurnDownMusicVolume);

        sprites = new Sprite[]
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

        Draw();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        Erase();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameManager.Instance.Drawer.RegisterUI(sprites[i]);
        }
        RegisterButtons();
    }

    public override void Erase()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameManager.Instance.Drawer.UnregisterUI(sprites[i]);
        }
        UnregisterButtons();
    }

    private static Sprite CreateString(Vector2 position, string name, Keys key)
    {
        var widthName = 500;
        var line = new Sprite(Textures.SettingsString, position, Layers.UIBackground);
        line.AddText(new Text(new Rectangle(position.ToPoint(), new Point(widthName, 60)), name, Align.Left, 10));
        line.AddText(new Text(new Rectangle((int)position.X + widthName, (int)position.Y, 250, 60), key.ToString(), Align.Center, 0));
        return line;
    }
}