using Core.Extensions;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class Button : BaseButton
{
    private readonly float buttonLayer;
    private readonly Texture2D texture;
    private readonly Color selectedColor;

    public Button(Texture2D texture, Vector2 position)
    {
        this.texture = texture;
        Position = position;
        buttonLayer = Layers.UI;
        selectedColor = Color.LightGray;
    }

    public Button(Texture2D texture, Vector2 position, Color selectedColor) : this(texture, position)
    {
        this.selectedColor = selectedColor;
    }

    public Button(Texture2D texture, Vector2 position, float buttonLayer) : this(texture, position)
    {
        this.buttonLayer = buttonLayer;
    }

    protected override void DrawButton(SpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        var defaultColor = Color.White;
        var color = Inactive
            ? defaultColor.WithAlpha(200).WithColorDelta(190)
            : Selected ? selectedColor : defaultColor;
        spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, buttonLayer);
    }

    protected override bool InBounds(Vector2 relativeMousePosition)
    {
        var width = texture.Width * Scale;
        var height = texture.Height * Scale;
        var bounds = new Rectangle(0, 0, (int)width, (int)height);
        return bounds.Contains(relativeMousePosition);
    }
}