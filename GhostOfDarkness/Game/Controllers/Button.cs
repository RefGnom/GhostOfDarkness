using System;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers;

public class Button : IComponent
{
    protected readonly Texture2D Texture;
    protected readonly Vector2 Position;
    private readonly string text;
    protected readonly Color SelectedColor;
    protected readonly float ButtonLayer;
    protected readonly float TextLayer;
    protected float Scale;

    public bool Selected { get; set; }

    public event Action OnClicked;

    public Button(Texture2D texture, Vector2 position, string text)
    {
        this.Texture = texture;
        this.Position = position;
        this.text = text;
        ButtonLayer = Layers.UI;
        TextLayer = Layers.Text;
        SelectedColor = Color.LightGray;
    }

    public Button(Texture2D texture, Vector2 position, string text, Color selectedColor) : this(texture, position, text)
    {
        this.SelectedColor = selectedColor;
    }

    public Button(Texture2D texture, Vector2 position, string text, float buttonLayer, float textLayer, Color selectedColor) : this(texture, position, text, selectedColor)
    {
        this.ButtonLayer = buttonLayer;
        this.TextLayer = textLayer;
    }

    public Button(Texture2D texture, Vector2 position, string text, float buttonLayer, float textLayer) : this(texture, position, text)
    {
        this.ButtonLayer = buttonLayer;
        this.TextLayer = textLayer;
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
        Scale = scale;
        DrawTexture(spriteBatch, scale);
        DrawText(spriteBatch, scale);
    }

    protected virtual void DrawTexture(SpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        var color = Selected ? SelectedColor : Color.White;
        spriteBatch.Draw(Texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, ButtonLayer);
    }

    protected virtual void DrawText(SpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        var textSize = Fonts.Buttons.MeasureString(text);
        var textPosition = position + new Vector2(Texture.Width / 2 - textSize.X / 2, Texture.Height / 2 - textSize.Y / 2) * scale;
        spriteBatch.DrawString(Fonts.Buttons, text, textPosition, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, TextLayer);
    }

    private bool InBounds(Vector2 mousePosition)
    {
        var width = Texture.Width * Scale;
        var height = Texture.Height * Scale;
        var x = Position.X * Scale;
        var y = Position.Y * Scale;
        var bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
        return bounds.Contains(mousePosition);
    }
}