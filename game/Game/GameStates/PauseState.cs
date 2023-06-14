using Microsoft.Xna.Framework;

namespace game;

internal class PauseState : GameState
{
    private Sprite background;

    public PauseState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState<PlayState>();
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
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
        switcher.SwitchState<SettingsState>();
    }

    public override void Play()
    {
        switcher.SwitchState<PlayState>();
    }

    public override void Dead()
    {
    }

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.UIBackground);

        var position = new Vector2(736, 370);
        var continueGame = new Button(Textures.ButtonBackground, position, "Resume");
        continueGame.OnClicked += Play;
        position.Y += 130;
        var settings = new Button(Textures.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        position.Y += 130;
        var mainMenu = new Button(Textures.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;

        buttons = new()
        {
            mainMenu,
            settings,
            continueGame
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