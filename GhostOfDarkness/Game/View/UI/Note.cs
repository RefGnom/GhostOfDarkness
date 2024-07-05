using Game.ContentLoaders;
using Game.Game;
using Game.Graphics;
using Game.Interfaces;
using Game.Managers;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.View.UI;

internal class Note : IDrawable, IInteractable
{
    private string text;
    private Vector2 backgroundScale;
    private readonly float interactionDistance = 40;
    private bool hintShown;
    private bool isOpen;

    public Vector2 Position { get; init; }
    public bool HasInteract => isOpen;
    public float InteractionCooldown => 0;

    public Note(Vector2 interactablePosition, string text, Vector2 backgroundScale)
    {
        Position = interactablePosition;
        this.text = text;
        this.backgroundScale = backgroundScale;
    }

    public void SetText(string text, Vector2 backgroundScale)
    {
        this.text = text;
        this.backgroundScale = backgroundScale;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        if (isOpen)
        {
            var texture = Textures.Paper;
            var origin = new Vector2(texture.Width / 2, texture.Height / 2) * backgroundScale;
            var topLeft = GameView.Center - origin;
            spriteBatch.DrawString(Fonts.Common12, text, topLeft + new Vector2(70, 50), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, Layers.Text);
            spriteBatch.Draw(texture, GameView.Center, null, Color.White, 0, origin, backgroundScale, SpriteEffects.None, Layers.UiBackground);
        }
    }

    public bool CanInteract(Vector2 target)
    {
        var distance = Vector2.Distance(Position, target);
        if (distance <= interactionDistance)
        {
            if (isOpen)
                HintManager.Hide();
            else
                HintManager.Show("Read - E");
            hintShown = true;
            return true;
        }
        isOpen = false;
        if (hintShown)
        {
            HintManager.Hide();
            hintShown = false;
        }
        return false;
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }

    public void Update(float deltaTime)
    {
    }
}