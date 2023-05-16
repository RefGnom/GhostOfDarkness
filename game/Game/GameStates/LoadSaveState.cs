using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class LoadSaveState : GameState
{
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

    public override void Draw(SpriteBatch spriteBatch)
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

    public override void Restart()
    {
    }

    public override void Save()
    {
    }

    public override void Start(IState previousState)
    {
        this.previousState = (GameState) previousState;

        var load = new Button(TexturesManager.ButtonBackground, new Vector2(40, 960), "Load");
        load.OnClicked += Play;
        var createNew = new Button(TexturesManager.ButtonBackground, new Vector2(538, 960), "Create new game");
        createNew.OnClicked += NewGame;
        var back = new Button(TexturesManager.ButtonBackground, new Vector2(1432, 960), "Back");
        back.OnClicked += Back;

        buttons = new()
        {
            load,
            createNew,
            back,
        };

        RegisterButtons();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        UnregisterButtons();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Update(float deltaTime)
    {
    }
}