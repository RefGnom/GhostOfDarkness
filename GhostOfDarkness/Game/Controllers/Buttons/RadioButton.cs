using System;
using Core.Extensions;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Buttons;

public class RadioButton : BaseButton
{
    private bool enabled;
    private Texture2D currentTexture;
    private Color currentColor;

    private readonly Texture2D disabledTexture;
    private readonly Texture2D enabledTexture;
    private readonly Color disabledColor;
    private readonly Color enabledColor;

    public bool Checkable { get; set; }

    public Action OnEnabled;
    public Action OnDisabled;

    public RadioButton(
        Texture2D disabledTexture,
        Texture2D enabledTexture,
        Vector2 position,
        Color? disabledColor = null,
        Color? enabledColor = null
    ) : base(position, Layers.UI)
    {
        if (disabledTexture.Bounds != enabledTexture.Bounds)
        {
            throw new ArgumentException("Disabled and enabled textures in radio button must be have equals size");
        }

        this.disabledTexture = disabledTexture;
        this.enabledTexture = enabledTexture;
        currentTexture = this.disabledTexture;
        this.disabledColor = disabledColor ?? Color.White;
        this.enabledColor = enabledColor ?? Color.White;
        currentColor = this.disabledColor;

        OnClicked += Swap;
    }

    public void Enable()
    {
        enabled = true;
        OnEnabled?.Invoke();
        currentTexture = enabledTexture;
        currentColor = enabledColor;
    }

    public void Disable()
    {
        enabled = false;
        OnDisabled?.Invoke();
        currentTexture = disabledTexture;
        currentColor = disabledColor;
    }

    protected override void DrawButton(SpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        var color = Selected && !enabled ? currentColor.WithColorDelta(230) : currentColor;
        spriteBatch.Draw(currentTexture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, Layer);
    }

    protected override Rectangle GetBounds() => disabledTexture.Bounds.Shift(Position);

    private void Swap()
    {
        if (enabled && Checkable)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }
}