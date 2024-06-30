using System;
using game;
using Game.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = game.IDrawable;

namespace Game.View;

internal class Text : IDrawable
{
    private readonly Rectangle bounds;
    private readonly Align align;
    private readonly int indent;
    private readonly float layer;
    private Vector2 position;
    private SpriteFont font;

    public string Value { get; private set; }
    public SpriteFont Font {
        get => font;
        set
        {
            font = value;
            SetText(Value);
        }
    }

    public Text(Rectangle bounds, string text, Align align, int indent, SpriteFont font, float? layer = null)
    {
        this.bounds = bounds;
        this.align = align;
        this.indent = indent;
        Value = text;
        Font = font;
        this.layer = layer ?? Layers.Text;
    }

    public void SetText(string text)
    {
        var size = font.MeasureString(text);
        position = bounds.Location.ToVector2();
        position.Y += (bounds.Height - size.Y) / 2;

        var indentX = align switch
        {
            Align.Left => indent,
            Align.Center => (bounds.Width - size.X) / 2,
            Align.Right => bounds.Width - size.X - indent,
            _ => throw new ArgumentOutOfRangeException($"Unknown align {align}")
        };
        position.X += indentX;
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.DrawString(font, Value, position * scale, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, layer);
    }
}