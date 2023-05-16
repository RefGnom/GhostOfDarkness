using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class PauseState : GameState
{
    private Sprite background;

    public PauseState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        throw new System.NotImplementedException();
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
        background = new Sprite(TexturesManager.PauseBackground, new Vector2(564, 312));

        var position = new Vector2(736, 370);
        var mainMenu = new Button(TexturesManager.ButtonBackground, position, "In main menu");
        mainMenu.OnClicked += Exit;
        position.Y += 130;
        var settings = new Button(TexturesManager.ButtonBackground, position, "Settings");
        settings.OnClicked += OpenSettings;
        position.Y += 130;
        var continueGame = new Button(TexturesManager.ButtonBackground, position, "Continue");
        continueGame.OnClicked += Play;

        buttons = new()
        {
            mainMenu,
            settings,
            continueGame
        };

        GameManager.Instance.Drawer.RegisterUI(background);
        RegisterButtons();
        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        GameManager.Instance.Drawer.UnregisterUI(background);
        UnregisterButtons();
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Update(float deltaTime)
    {
    }
}