using Microsoft.Xna.Framework;

namespace game;

internal class SettingsState : GameState
{
    private Sprite background;

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
        background = new Sprite(TexturesManager.Background, Vector2.Zero, Layers.Background);

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
        GameManager.Instance.Drawer.RegisterUI(background);
        RegisterButtons();
    }

    public override void Erase()
    {
        GameManager.Instance.Drawer.UnregisterUI(background);
        UnregisterButtons();
    }
}