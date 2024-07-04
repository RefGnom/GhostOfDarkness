using game;
using Game.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = game.IDrawable;

namespace Game.View;

public class Text : IDrawable
{
    private readonly Rectangle bounds;
    private readonly Align align;
    private readonly int indentX;
    private readonly int indentY;
    private readonly float layer;
    private Vector2 position;
    private SpriteFont font;

    public string Value { get; private set; }

    public SpriteFont Font
    {
        get => font;
        set
        {
            font = value;
            SetText(Value);
        }
    }

    public Text(Rectangle bounds, string text, SpriteFont font, Align align = 0, int indentX = 0, int indentY = 0, float? layer = null)
    {
        this.bounds = bounds;
        Value = text;
        this.align = align;
        this.indentX = indentX;
        this.indentY = indentY;
        Font = font;
        this.layer = layer ?? Layers.Text;
    }

    public void SetText(string text)
    {
        var size = font.MeasureString(text);
        position = bounds.Location.ToVector2();
        position.Y += (bounds.Height - size.Y) / 2;
        position.X += (bounds.Width - size.X) / 2;

        var shiftY = (bounds.Height - size.Y) / 2 - indentY;
        var shiftYCoefficient = IsFlagged(Align.Down) - IsFlagged(Align.Up);
        position.Y += shiftY * shiftYCoefficient;

        var shiftX = (bounds.Width - size.X) / 2 - indentX;
        var shiftXCoefficient = IsFlagged(Align.Right) - IsFlagged(Align.Left);
        position.X += shiftX * shiftXCoefficient;

        return;

        int IsFlagged(Align alignCompare) => (align & alignCompare) != 0 ? 1 : 0;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.DrawString(font, Value, position * scale, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layer);
    }
}