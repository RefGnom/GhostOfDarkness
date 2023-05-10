using game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.View;

internal static class Drawer
{
    private static List<IDrawable> drawables = new();

    public static void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public static void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}