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

    private readonly Texture2D disabledTexture;
    private readonly Texture2D enabledTexture;

    public Action OnEnabled;
    public Action OnDisabled;

    public RadioButton(
        Texture2D disabledTexture,
        Texture2D enabledTexture,
        Vector2 position
    ) : base(position, Layers.UI)
    {
        if (disabledTexture.Bounds != enabledTexture.Bounds)
        {
            throw new ArgumentException("Disabled and enabled textures in radio button must be have equals size");
        }

        this.disabledTexture = disabledTexture;
        this.enabledTexture = enabledTexture;
        currentTexture = this.disabledTexture;

        OnClicked += Swap;
    }

    public void Enable()
    {
        enabled = true;
        OnEnabled?.Invoke();
        currentTexture = enabledTexture;
    }

    public void Disable()
    {
        enabled = false;
        OnDisabled?.Invoke();
        currentTexture = disabledTexture;
    }

    protected override void DrawButton(SpriteBatch spriteBatch, float scale)
    {
        var position = Position * scale;
        spriteBatch.Draw(currentTexture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, Layer);
    }

    protected override Rectangle GetBounds() => disabledTexture.Bounds.Shift(Position);

    private void Swap()
    {
        if (enabled)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }
}