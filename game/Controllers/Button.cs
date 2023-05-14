using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game;

internal class Button : IDrawable
{
    private Texture2D texture;
    private readonly Vector2 position;
    private readonly string text;
    private float scale;

    public event Action OnClicked;

    public Button(Texture2D texture, Vector2 position, string text)
    {
        this.texture = texture;
        this.position = position;
        this.text = text;
    }

    public bool Clicked()
    {
        if (MouseController.LeftButtonClicked() && InBounds(MouseController.WindowPosition))
        {
            OnClicked?.Invoke();
            return true;
        }
        return false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        scale = GameManager.Instance.Game.WindowWidth / 1920f;
        var position = this.position * scale;
        spriteBatch.Draw(TexturesManager.ButtonBackground, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.UI);
        var textSize = FontsManager.TimesNewRoman.MeasureString(text);
        var textPosition = position + new Vector2(texture.Width / 2 - textSize.X / 2, texture.Height / 2 - textSize.Y / 2) * scale;
        spriteBatch.DrawString(FontsManager.TimesNewRoman, text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Text);
    }

    private bool InBounds(Vector2 mousePosition)
    {
        var width = texture.Width * scale;
        var height = texture.Height * scale;
        var x = position.X * scale;
        var y = position.Y * scale;
        var bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
        return bounds.Contains(mousePosition);
    }
}