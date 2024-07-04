using Core.Extensions;
using game;
using Game.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class Button : BaseButton
{
    private readonly Texture2D texture;
    private readonly Color selectedColor;

    public Button(Texture2D texture, Vector2 position) : base(position, Layers.UI)
    {
        this.texture = texture;
        selectedColor = Color.LightGray;
    }

    public Button(Texture2D texture, Vector2 position, Color selectedColor) : this(texture, position)
    {
        this.selectedColor = selectedColor;
    }

    public Button(Texture2D texture, Vector2 position, float buttonLayer) : this(texture, position)
    {
        Layer = buttonLayer;
    }

    protected override void DrawButton(ISpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        var defaultColor = Color.White;
        var color = Inactive
            ? defaultColor.WithAlpha(200).WithColorDelta(190)
            : Selected ? selectedColor : defaultColor;
        spriteBatch.Draw(texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, Layer);
    }

    protected override Rectangle GetBounds() => texture.Bounds.Shift(Position);
}