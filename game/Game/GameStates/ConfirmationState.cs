using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class ConfirmationState : GameState
{
    private Sprite background;

    public ConfirmationState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
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

    public override void Draw(SpriteBatch spriteBatch, float scale)
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
        background = new Sprite(TexturesManager.PauseBackground, new Vector2(564, 312));

        var position = new Vector2(736, 430);
        var confirm = new Button(TexturesManager.ButtonBackground, position, "Confirm");
        confirm.OnClicked += Confirm;
        position.Y += 140;
        var cancel = new Button(TexturesManager.ButtonBackground, position, "Cancel");
        cancel.OnClicked += Back;

        buttons = new()
        {
            confirm,
            cancel
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