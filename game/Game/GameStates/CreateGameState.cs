using Microsoft.Xna.Framework;

namespace game;

internal class CreateGameState : GameState
{
    private TextInput textInput;
    private Sprite background;

    public CreateGameState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
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
    }

    public override void NewGame()
    {
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

    private void CreateGame()
    {
        switcher.StartGame();
        Play();
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(TexturesManager.Background, Vector2.Zero, Layers.Background);

        var back = new Button(TexturesManager.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;

        var create = new Button(TexturesManager.ButtonBackground, new Vector2(380, 600), "Create");
        create.OnClicked += CreateGame;

        textInput = new TextInput(TexturesManager.FieldForText, new Vector2(380, 466));

        buttons = new()
        {
            back,
            create
        };

        Draw();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        Erase();
        textInput.Delete();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw()
    {
        GameManager.Instance.Drawer.RegisterUI(background);
        GameManager.Instance.Drawer.RegisterUI(textInput);
        RegisterButtons();
    }

    public override void Erase()
    {
        GameManager.Instance.Drawer.UnregisterUI(background);
        GameManager.Instance.Drawer.UnregisterUI(textInput);
        UnregisterButtons();
    }
}