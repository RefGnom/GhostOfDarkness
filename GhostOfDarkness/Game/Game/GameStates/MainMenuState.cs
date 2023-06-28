using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class MainMenuState : GameState
{
    private readonly Button newGame;
    private readonly Button loadSave;
    private readonly Button settings;
    private readonly Button exit;

    public MainMenuState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        drawables.Add(new Sprite(Textures.Background, Vector2.Zero, Layers.Background));

        var buttonDistance = 150;
        var position = new Vector2(160, 275);
        newGame = new Button(Textures.ButtonBackground, position, "New Game");
        newGame.OnClicked += NewGame;
        components.Add(newGame);

        position.Y += buttonDistance;
        loadSave = new Button(Textures.ButtonBackground, position, "Load Game");
        loadSave.OnClicked += LoadSave;
        components.Add(loadSave);

        position.Y += buttonDistance;
        settings = new Button(Textures.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        components.Add(settings);

        position.Y += buttonDistance;
        exit = new Button(Textures.ButtonBackground, position, "Exit");
        exit.OnClicked += Exit;
        components.Add(exit);
    }

    public override void Back()
    {
        switcher.SwitchState<ConfirmationState>();
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

    public override void Start(GameState previousState)
    {
        if (previousState.IsConfirmed)
            GameIsExit = true;
    }

    public override void Stop()
    {
        newGame.Selected = false;
        loadSave.Selected = false;
        settings.Selected = false;
        exit.Selected = false;
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        base.Draw(spriteBatch, scale);
    }
}