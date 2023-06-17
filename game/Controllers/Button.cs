using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game;

internal class Button : IDrawable
{
    private readonly Texture2D texture;
    private readonly Vector2 position;
    private readonly string text;
    private float scale;
    private readonly float buttonLayer;
    private readonly float textLayer;

    public event Action OnClicked;

    public Button(Texture2D texture, Vector2 position, string text)
    {
        this.texture = texture;
        this.position = position;
        this.text = text;
        buttonLayer = Layers.UI;
        textLayer = Layers.Text;
    }

    public Button(Texture2D texture, Vector2 position, string text, float buttonLayer, float textLayer)
    {
        this.texture = texture;
        this.position = position;
        this.text = text;
        this.buttonLayer = buttonLayer;
        this.textLayer = textLayer;
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

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        this.scale = scale;
        var position = this.position * scale;
        spriteBatch.Draw(Textures.ButtonBackground, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, buttonLayer);
        var textSize = Fonts.Buttons.MeasureString(text);
        var textPosition = position + new Vector2(texture.Width / 2 - textSize.X / 2, texture.Height / 2 - textSize.Y / 2) * scale;
        spriteBatch.DrawString(Fonts.Buttons, text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, textLayer);
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