using game;
using Game.Controllers.Buttons;
using Game.Interfaces;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Game.GameStates;

internal class ConfirmationState : GameState
{
    public ConfirmationState(IGameStateSwitcher stateSwitcher, IButtonFactory buttonFactory) : base(stateSwitcher)
    {
        Drawables.Add(new Sprite(Textures.PauseBackground, new Vector2(564, 312), Layers.ConfirmationWindowBackground));

        var position = new Vector2(736, 430);
        var confirm = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, position, "Confirm", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        confirm.OnClicked += Confirm;
        Components.Add(confirm);

        position.Y += 140;
        var cancel = buttonFactory.CreateButtonWithText(Textures.ButtonBackground, position, "Cancel", Layers.ConfirmationWindowUI, Layers.ConfirmationWindowText);
        cancel.OnClicked += Back;
        Components.Add(cancel);
    }

    public override void Back()
    {
        Switcher.SwitchState(PreviousState);
    }

    public override void Confirm()
    {
        IsConfirmed = true;
        Switcher.SwitchState(PreviousState);
    }

    public override void Start(GameState previousState)
    {
    }

    public override void Stop()
    {
    }

    public override void Draw(SpriteBatch spriteBatch, float scale)
    {
        PreviousState.Draw(spriteBatch, scale);
        base.Draw(spriteBatch, scale);
    }
}