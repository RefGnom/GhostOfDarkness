using game;
using Game.Controllers;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;

namespace Game.Game.GameStates;

internal class MainMenuState : GameState
{
    private readonly Button newGame;
    private readonly Button loadSave;
    private readonly Button settings;
    private readonly Button exit;

    public MainMenuState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        const int buttonDistance = 150;
        var position = new Vector2(160, 275);
        newGame = new Button(Textures.ButtonBackground, position, "New Game");
        newGame.OnClicked += NewGame;
        Components.Add(newGame);

        position.Y += buttonDistance;
        loadSave = new Button(Textures.ButtonBackground, position, "Load Game");
        loadSave.OnClicked += LoadSave;
        Components.Add(loadSave);

        position.Y += buttonDistance;
        settings = new Button(Textures.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        Components.Add(settings);

        position.Y += buttonDistance;
        exit = new Button(Textures.ButtonBackground, position, "Exit");
        exit.OnClicked += Exit;
        Components.Add(exit);
    }

    public override void Back()
    {
        Switcher.SwitchState<ConfirmationState>();
    }

    public override void Exit()
    {
        Switcher.SwitchState<ConfirmationState>();
    }

    public override void NewGame()
    {
        Switcher.SwitchState<CreateGameState>();
    }

    public override void LoadSave()
    {
        Switcher.SwitchState<LoadSaveState>();
    }

    public override void OpenSettings()
    {
        Switcher.SwitchState<SettingsState>();
    }

    public override void Start(GameState previousState)
    {
        if (previousState.IsConfirmed)
        {
            GameIsExit = true;
        }
    }

    public override void Stop()
    {
        newGame.Selected = false;
        loadSave.Selected = false;
        settings.Selected = false;
        exit.Selected = false;
    }
}