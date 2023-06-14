using Microsoft.Xna.Framework;

namespace game;

internal class ConfirmationState : GameState
{
    private Sprite background;

    public ConfirmationState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
    }

    public override void Back()
    {
        switcher.SwitchState(previousState);
    }

    public override void Confirm()
    {
        IsConfirmed = true;
        switcher.SwitchState(previousState);
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
        background = new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.ConfirmationWindowBackground);

        var position = new Vector2(736, 430);
        var confirm = new Button(Textures.ButtonBackground, position, "Confirm", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        confirm.OnClicked += Confirm;
        position.Y += 140;
        var cancel = new Button(Textures.ButtonBackground, position, "Cancel", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        cancel.OnClicked += Back;

        buttons = new()
        {
            confirm,
            cancel
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
        previousState.Draw();
        GameManager.Instance.Drawer.RegisterUI(background);
        RegisterButtons();
    }

    public override void Erase()
    {
        previousState.Erase();
        GameManager.Instance.Drawer.UnregisterUI(background);
        UnregisterButtons();
    }
}