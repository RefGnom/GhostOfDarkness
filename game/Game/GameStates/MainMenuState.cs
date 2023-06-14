using Microsoft.Xna.Framework;

namespace game;

internal class MainMenuState : GameState
{
    private Sprite background;

    public MainMenuState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
    }

    public override void Confirm()
    {
    }

    public override void Exit()
    {
        switcher.SwitchState<ConfirmationState>();
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void LoadSave()
    {
        switcher.SwitchState<LoadSaveState>();
    }

    public override void OpenSettings()
    {
        switcher.SwitchState<SettingsState>();
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
    }

    public override void Start(GameState previousState)
    {
        if (previousState.IsConfirmed)
            GameIsExit = true;

        background = new Sprite(Textures.Background, Vector2.Zero, Layers.Background);

        var buttonDistance = 150;
        var position = new Vector2(160, 275);
        var newGame = new Button(Textures.ButtonBackground, position, "New Game");
        newGame.OnClicked += NewGame;

        position.Y += buttonDistance;
        var loadSave = new Button(Textures.ButtonBackground, position, "Load Game");
        loadSave.OnClicked += LoadSave;

        position.Y += buttonDistance;
        var settings = new Button(Textures.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;

        position.Y += buttonDistance;
        var exit = new Button(Textures.ButtonBackground, position, "Exit");
        exit.OnClicked += Exit;

        buttons = new()
        {
            newGame,
            loadSave,
            settings,
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