using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Game.Enums;
using Game.View;

namespace game;

internal class SwitcherObject : Text
{
    private bool active;

    public bool Active => active;

    public event Action OnActivated;
    public event Action OnDeactivated;

    public SwitcherObject(Rectangle bounds, string text, Align align, int indent, SpriteFont font) : base(bounds, text, align, indent, font)
    {

    }

    public void SetActive(bool active)
    {
        if (active && !this.active)
            OnActivated?.Invoke();
        if (!active && this.active)
            OnDeactivated?.Invoke();
        this.active = active;
    }
}