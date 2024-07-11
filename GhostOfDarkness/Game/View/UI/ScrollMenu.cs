using System.Collections.Generic;
using Game.Graphics;
using Game.Interfaces;
using Microsoft.Xna.Framework;

namespace Game.View.UI;

public class ScrollMenu : IComponent
{
    private readonly Rectangle bounds;
    private readonly List<IComponent> components;

    public ScrollMenu(Rectangle bounds)
    {
        this.bounds = bounds;
        components = [];
    }

    public ScrollMenu AppendComponent(IComponent component)
    {
        components.Add(component);
        return this;
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch, scale);
        }
    }

    public void Update(float deltaTime)
    {
        foreach (var component in components)
        {
            component.Update(deltaTime);
        }
    }
}