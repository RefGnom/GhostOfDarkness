using System.Collections.Generic;
using Game.ContentLoaders;
using Game.Controllers;
using Game.Controllers.InputServices;
using Game.Graphics;
using Game.Interfaces;
using Microsoft.Xna.Framework;

namespace Game.View.UI;

public class ScrollMenu : IComponent
{
    private readonly Rectangle bounds;
    private readonly ScrollBounds scrollBounds;
    private readonly ScrollBar scrollBar;
    private readonly List<IComponent> components;

    public ScrollMenu(Rectangle bounds, Vector2 scrollBarPosition)
    {
        this.bounds = bounds;
        scrollBounds = new ScrollBounds(Input.MouseService, bounds);
        scrollBar = new ScrollBar(
            scrollBarPosition,
            Textures.ScrollBar,
            Textures.ScrollBox,
            new Point(1, 2)
        );
        scrollBounds.OnScroll += scrollValue =>
        {
            const int percentsPerShift = 10;
            var shiftValue = scrollValue * percentsPerShift / 100f;
            scrollBar.ShiftBox(shiftValue);
        };
        components = [];
    }

    public ScrollMenu AppendComponent(IComponent component)
    {
        components.Add(component);
        return this;
    }

    public void ClearComponents()
    {
        components.Clear();
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        scrollBar.Draw(spriteBatch, scale);
        scrollBounds.SetScale(scale);
        foreach (var component in components)
        {
            component.Draw(spriteBatch, scale);
        }
    }

    public void Update(float deltaTime)
    {
        scrollBar.Update(deltaTime);
        scrollBounds.Update(deltaTime);
        foreach (var component in components)
        {
            component.Update(deltaTime);
        }
    }
}