using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game;

internal class Text : IDrawable
{
    private readonly Rectangle bounds;
    private string text;
    private readonly Align align;
    private readonly int indent;
    private Vector2 position;
    private readonly SpriteFont font;

    public Text(Rectangle bounds, string text, Align align, int indent)
    {
        this.bounds = bounds;
        this.align = align;
        this.indent = indent;
        font = Fonts.TimesNewRoman;
        SetText(text);
    }

    public void SetText(string text)
    {
        this.text = text;
        var size = font.MeasureString(text);
        position = bounds.Location.ToVector2();
        position.Y += (bounds.Height - size.Y) / 2;

        if (align == Align.Left)
            position.X += indent;
        if (align == Align.Center)
            position.X += (bounds.Width - size.X) / 2;
        if (align == Align.Right)
            position.X += bounds.Width - size.X - indent;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.DrawString(font, text, position * scale, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layers.Text);
    }
}