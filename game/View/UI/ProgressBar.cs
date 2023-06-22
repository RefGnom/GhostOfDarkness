using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game;

internal class ProgressBar : IComponent, IDrawable
{
    private readonly float minValue;
    private readonly float maxValue;
    private readonly Vector2 position;
    private readonly Vector2 groundOrigin;
    private readonly Vector2 valueOrigin;
    private readonly Rectangle bounds;
    private readonly Texture2D background;
    private readonly Texture2D foreground;
    private readonly Texture2D value;
    private readonly int indent = 10;
    private bool active;
    private float lastScale;
    private float xValue;

    public float Value { get; private set; }
    public event Action<float> ValueOnChanged;

    public ProgressBar(float minValue, float maxValue, Vector2 position)
        : this(minValue, maxValue, position, Textures.ProgressBarBackground, Textures.ProgressBarForeground, Textures.ProgressBarValue)
    { }

    public ProgressBar(float minValue, float maxValue, Vector2 position, Texture2D background, Texture2D foreground, Texture2D value)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        groundOrigin = new Vector2(0, background.Height / 2);
        valueOrigin = new Vector2(-value.Width / 2, value.Height / 2);
        bounds = background.Bounds;
        bounds.Location = position.ToPoint();
        position.Y += background.Height / 2;
        this.position = position;
        this.background = background;
        this.foreground = foreground;
        this.value = value;
    }

    public void Update(float deltaTime)
    {
        if (MouseController.LeftButtonClicked() && MauseInBounds())
            active = true;
        if (MouseController.LeftButtonUnclicked())
            active = false;

        if (active)
        {
            UpdateValue();
            ValueOnChanged?.Invoke(Value);
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(background, position * scale, null, Color.White, 0, groundOrigin, scale, SpriteEffects.None, Layers.UIBackground);
        spriteBatch.Draw(foreground, position * scale, new Rectangle(0, 0, (int)xValue, bounds.Height), Color.White, 0, groundOrigin, scale, SpriteEffects.None, Layers.UI);
        spriteBatch.Draw(value, new Vector2(xValue, position.Y) * scale, null, Color.White, 0, valueOrigin, scale, SpriteEffects.None, Layers.Text);
        lastScale = scale;
    }

    private bool MauseInBounds()
    {
        var mouse = MouseController.WindowPosition / lastScale;
        return bounds.Contains(mouse);
    }

    private void UpdateValue()
    {
        var width = bounds.Width - indent;
        xValue = MouseController.WindowPosition.X - bounds.X;

        if (xValue < 0)
            xValue = 0;
        if (xValue > width * lastScale)
            xValue = width * lastScale;

        xValue /= lastScale;
        Value = minValue + xValue * maxValue / width;
    }
}