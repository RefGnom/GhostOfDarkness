using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class ConfirmationState : GameState
{
    public ConfirmationState(IGameStateSwitcher stateSwitcher) : base(stateSwitcher)
    {
        drawables.Add(new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.ConfirmationWindowBackground));

        var position = new Vector2(736, 430);
        var confirm = new Button(Textures.ButtonBackground, position, "Confirm", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        confirm.OnClicked += Confirm;
        components.Add(confirm);

        position.Y += 140;
        var cancel = new Button(Textures.ButtonBackground, position, "Cancel", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        cancel.OnClicked += Back;
        components.Add(cancel);
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
    }

    public override void Stop()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        previousState.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}