using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game;

internal class Button : IComponent
{
    private readonly Texture2D texture;
    private readonly Vector2 position;
    private readonly string text;
    private readonly Color selectedColor;
    private readonly float buttonLayer;
    private readonly float textLayer;
    private float scale;

    public bool Selected { get; set; }

    public event Action OnClicked;

    public Button(Texture2D texture, Vector2 position, string text)
    {
        this.texture = texture;
        this.position = position;
        this.text = text;
        buttonLayer = Layers.UI;
        textLayer = Layers.Text;
        selectedColor = Color.LightGray;
    }

    public Button(Texture2D texture, Vector2 position, string text, Color selectedColor) : this(texture, position, text)
    {
        this.selectedColor = selectedColor;
    }

    public Button(Texture2D texture, Vector2 position, string text, float buttonLayer, float textLayer, Color selectedColor) : this(texture, position, text, selectedColor)
    {
        this.buttonLayer = buttonLayer;
        this.textLayer = textLayer;
    }

    public Button(Texture2D texture, Vector2 position, string text, float buttonLayer, float textLayer) : this(texture, position, text)
    {
        this.buttonLayer = buttonLayer;
        this.textLayer = textLayer;
    }

    public void Update(float deltaTime)
    {
        if (InBounds(MouseController.WindowPosition))
        {
            Selected = true;
            if (MouseController.LeftButtonClicked())
            {
                OnClicked?.Invoke();
            }
        }
        else
        {
            Selected = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        this.scale = scale;
        var position = this.position * scale;
        var color = Selected ? selectedColor : Color.White;
        spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, buttonLayer);
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