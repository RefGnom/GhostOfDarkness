using Microsoft.Xna.Framework;

namespace game;

internal class LoadSaveState : GameState
{
    private Sprite savesStorage;
    private Sprite background;

    public LoadSaveState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState<MainMenuState>();
    }

    public override void Confirm()
    {
    }

    public override void LoadSave()
    {
    }

    public override void Exit()
    {
    }

    public override void NewGame()
    {
        switcher.SwitchState<CreateGameState>();
    }

    public override void OpenSettings()
    {
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
        savesStorage = new Sprite(TexturesManager.SavesWindow, new Vector2(40, 70), Layers.UIBackground);
        background = new Sprite(TexturesManager.Background, Vector2.Zero, Layers.Background);

        var load = new Button(TexturesManager.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        var createNew = new Button(TexturesManager.ButtonBackground, new Vector2(538, 960), "Create New Game");
        createNew.OnClicked += NewGame;
        var back = new Button(TexturesManager.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;

        buttons = new()
        {
            load,
            createNew,
            back,
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
        GameManager.Instance.Drawer.RegisterUI(savesStorage);
        RegisterButtons();
    }

    public override void Erase()
    {
        GameManager.Instance.Drawer.UnregisterUI(background);
        GameManager.Instance.Drawer.UnregisterUI(savesStorage);
        UnregisterButtons();
    }
}