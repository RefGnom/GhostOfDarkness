using game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.View;

internal class Drawer
{
    private List<IDrawable> drawables = new();

    public void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}