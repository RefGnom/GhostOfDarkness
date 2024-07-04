using System.Collections.Generic;
using Game.Graphics;
using Game.Interfaces;

namespace Game.Managers;

internal class Drawer
{
    private readonly List<IDrawable> drawables = [];
    private readonly List<IDrawable> uiDrawables = [];
    private readonly List<IDrawable> hudDrawables = [];

    public void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void RegisterUi(IDrawable drawable)
    {
        uiDrawables.Add(drawable);
    }

    public void RegisterHud(IDrawable drawable)
    {
        hudDrawables.Add(drawable);
    }

    public void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public void UnregisterUi(IDrawable drawable)
    {
        uiDrawables.Remove(drawable);
    }

    public void UnregisterHud(IDrawable drawable)
    {
        hudDrawables.Remove(drawable);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    public void DrawUi(ISpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in uiDrawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    public void DrawHud(ISpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in hudDrawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }
}