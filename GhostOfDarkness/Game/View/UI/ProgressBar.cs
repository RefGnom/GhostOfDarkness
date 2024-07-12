using System;
using Game.ContentLoaders;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Interfaces;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.View.UI;

internal class ProgressBar : IComponent
{
    private readonly IMouseService mouseService;
    private readonly float minValue;
    private readonly float maxValue;
    private readonly Vector2 position;
    private readonly Vector2 groundOrigin;
    private readonly Vector2 valueOrigin;
    private readonly Rectangle bounds;
    private readonly Texture2D background;
    private readonly Texture2D foreground;
    private readonly Texture2D value;
    private readonly int indent;
    private bool active;
    private float lastScale;
    private float xValue;

    public float Value { get; private set; }
    public event Action<float> ValueOnChanged;

    public ProgressBar(IMouseService mouseService, float minValue, float maxValue, Vector2 position, int indent = 0)
        : this(
            mouseService,
            minValue,
            maxValue,
            position,
            Textures.ProgressBarBackground,
            Textures.ProgressBarForeground,
            Textures.ProgressBarValue,
            indent
        )
    {
    }

    public ProgressBar(
        IMouseService mouseService,
        float minValue,
        float maxValue,
        Vector2 position,
        Texture2D background,
        Texture2D foreground,
        Texture2D value,
        int indent = 0
    )
    {
        this.mouseService = mouseService;
        this.minValue = minValue;
        this.maxValue = maxValue;
        groundOrigin = new Vector2(0, background.Height / 2f);
        valueOrigin = new Vector2(value.Width / 2f, value.Height / 2f);
        bounds = background.Bounds;
        bounds.Location = position.ToPoint();
        position.Y += background.Height / 2f;
        this.position = position;
        this.background = background;
        this.foreground = foreground;
        this.value = value;
        this.indent = indent;
    }

    public void SetValue(float value)
    {
        if (value < minValue || value > maxValue)
        {
            throw new ArgumentException($"Value should be between {minValue} {maxValue}");
        }

        Value = value;
        ValueOnChanged?.Invoke(Value);
        xValue = (Value - minValue) * (bounds.Width - indent) / maxValue;
    }

    public void Update(float deltaTime)
    {
        if (mouseService.LeftButtonClicked() && MouseInBounds())
        {
            active = true;
        }

        if (mouseService.LeftButtonReleased())
        {
            active = false;
        }

        if (active)
        {
            UpdateValue();
            ValueOnChanged?.Invoke(Value);
        }
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        spriteBatch.Draw(background, position * scale, null, Color.White, 0, groundOrigin, scale, SpriteEffects.None, Layers.UiBackground);
        spriteBatch.Draw(foreground, position * scale, new Rectangle(0, 0, (int)xValue, bounds.Height), Color.White, 0, groundOrigin, scale, SpriteEffects.None,
            Layers.Ui);
        spriteBatch.Draw(value, new Vector2(position.X + xValue, position.Y) * scale, null, Color.White, 0, valueOrigin, scale, SpriteEffects.None,
            Layers.Text);
        lastScale = scale;
    }

    private bool MouseInBounds()
    {
        var mouse = mouseService.GetWindowPosition() / lastScale;
        // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable
        return bounds.Contains(mouse);
    }

    private void UpdateValue()
    {
        var width = bounds.Width - indent;
        xValue = mouseService.GetWindowPosition().X - bounds.X * lastScale;

        if (xValue < indent)
        {
            xValue = indent;
        }

        if (xValue > width * lastScale)
        {
            xValue = width * lastScale;
        }

        xValue /= lastScale;
        Value = minValue + xValue * maxValue / width;
        if (Math.Abs(xValue - indent / lastScale) < 0.1f || Value < minValue)
        {
            Value = minValue;
        }

        if (Value > maxValue)
        {
            Value = maxValue;
        }
    }
}