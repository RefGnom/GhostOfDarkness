using Microsoft.Xna.Framework;

namespace game;

internal class SettingsState : GameState
{
    private Sprite[] sprites;

    public SettingsState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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
        var background = new Sprite(TexturesManager.Background, Vector2.Zero, Layers.Background);

        var position = new Vector2(30, 30);
        var string1 = CreateString(position, "On full screen", Settings.SwitchScreen.ToString());
        position.Y += 60;
        var string2 = CreateString(position, "Open menu", Settings.OpenMenu.ToString());
        position.Y += 60;
        var string3 = CreateString(position, "Move Up", Settings.Up.ToString());
        position.Y += 60;
        var string4 = CreateString(position, "Move Down", Settings.Down.ToString());
        position.Y += 60;
        var string5 = CreateString(position, "Move Left", Settings.Left.ToString());
        position.Y += 60;
        var string6 = CreateString(position, "Move Right", Settings.Right.ToString());

        sprites = new Sprite[]
        {
            background,
            string1,
            string2,
            string3,
            string4,
            string5,
            string6
        };

        var save = new Button(TexturesManager.ButtonBackground, new Vector2(934, 960), "Save");
        save.OnClicked += Save;

        var exit = new Button(TexturesManager.ButtonBackground, new Vector2(1432, 960), "Exit");
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

    private static Sprite CreateString(Vector2 position, string name, string key)
    {
        var line = new Sprite(TexturesManager.SettingsString, position, Layers.UIBackground);
        line.AddText(new Text(new Rectangle(position.ToPoint(), new Point(600, 60)), name, Align.Left, 10));
        line.AddText(new Text(new Rectangle((int)position.X + 600, (int)position.Y, 200, 60), key, Align.Center, 0));
        return line;
    }
}