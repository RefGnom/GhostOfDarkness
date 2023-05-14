using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class Drawer
{
    private List<IDrawable> drawables = new();
    private List<IDrawable> HUDdrawables = new();

    public void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void RegisterHUD(IDrawable drawable)
    {
        HUDdrawables.Add(drawable);
    }

    public void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public void UnregisterHUD(IDrawable drawable)
    {
        HUDdrawables.Remove(drawable);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch);
        }
    }

    public void DrawHUD(SpriteBatch spriteBatch)
    {
        foreach (var drawable in HUDdrawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}