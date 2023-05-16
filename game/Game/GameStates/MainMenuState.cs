using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class MainMenuState : GameState
{
    public MainMenuState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
    }

    public override void Confirm()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(float deltaTime)
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

        var buttonDistance = 150;
        var position = new Vector2(160, 275);
        var newGame = new Button(TexturesManager.ButtonBackground, position, "New game");
        newGame.OnClicked += NewGame;
        position.Y += buttonDistance;
        var loadSave = new Button(TexturesManager.ButtonBackground, position, "Load save");
        loadSave.OnClicked += LoadSave;
        position.Y += buttonDistance;
        var settings = new Button(TexturesManager.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        position.Y += buttonDistance;
        var exit = new Button(TexturesManager.ButtonBackground, position, "Exit");
        exit.OnClicked += Exit;

        buttons = new()
        {
            newGame,
            loadSave,
            settings,
            exit
        };

        RegisterButtons();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        UnregisterButtons();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }
}