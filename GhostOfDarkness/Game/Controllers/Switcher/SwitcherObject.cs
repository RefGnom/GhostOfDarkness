using System;
using Core.DependencyInjection;
using Game.Enums;
using Game.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Controllers.Switcher;

[DiIgnore]
internal class SwitcherObject : Text
{
    private bool active;

    public bool Active => active;

    public event Action OnActivated;
    public event Action OnDeactivated;

    public SwitcherObject(Rectangle bounds, string text, Align align, int indentX, SpriteFont font) : base(bounds, text, font, align, indentX)
    {
    }

    public void SetActive(bool active)
    {
        if (active && !this.active)
        {
            OnActivated?.Invoke();
        }

        if (!active && this.active)
        {
            OnDeactivated?.Invoke();
        }

        this.active = active;
    }
}