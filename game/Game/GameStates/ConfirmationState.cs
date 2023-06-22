using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        MouseController.LeftButtonOnClicked += ClickedButtons;
    }

    public override void Stop()
    {
        MouseController.LeftButtonOnClicked -= ClickedButtons;
    }

    public override void Update(float deltaTime)
    {
        previousState.Update(deltaTime);
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        background.Draw(spriteBatch, scale);
        previousState.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}