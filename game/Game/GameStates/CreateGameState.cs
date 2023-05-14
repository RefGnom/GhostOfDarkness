using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class CreateGameState : GameState
{
    private TextInput textInput;

    public CreateGameState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(IState previousState)
    {
        this.previousState = (GameState)previousState;
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

        GameManager.Instance.Drawer.RegisterUI(textInput);
        RegisterButtons();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        textInput.Delete();
        GameManager.Instance.Drawer.UnregisterUI(textInput);
        UnregisterButtons();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
    }

    public override void Update(float deltaTime)
    {
    }

    private void CreateGame()
    {
        // Создать новое сохранение
        Play();
    }
}