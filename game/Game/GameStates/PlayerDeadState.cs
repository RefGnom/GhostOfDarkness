using Microsoft.Xna.Framework;

namespace game;

internal class PlayerDeadState : GameState
{
    private Sprite background;

    public PlayerDeadState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
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
    }

    public override void Play()
    {
    }

    public override void Dead()
    {
    }

    public override void Restart()
    {
        switcher.StartGame();
        switcher.SwitchState<PlayState>();
    }

    public override void Save()
    {
    }

    public override void Start(GameState previousState)
    {
        background = new Sprite(TexturesManager.PauseBackground, new Vector2(564, 312), Layers.UIBackground);

        var position = new Vector2(736, 430);
        var restart = new Button(TexturesManager.ButtonBackground, position, "Restart");
        restart.OnClicked += Restart;
        position.Y += 140;
        var mainMenu = new Button(TexturesManager.ButtonBackground, position, "To Main Menu");
        mainMenu.OnClicked += Exit;

        buttons = new()
        {
            mainMenu,
            restart
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