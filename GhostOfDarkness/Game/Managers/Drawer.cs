﻿using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game;

internal class Drawer
{
    private readonly List<IDrawable> drawables = new();
    private readonly List<IDrawable> UIdrawables = new();
    private readonly List<IDrawable> HUDdrawables = new();

    public void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void RegisterUI(IDrawable drawable)
    {
        UIdrawables.Add(drawable);
    }

    public void RegisterHUD(IDrawable drawable)
    {
        HUDdrawables.Add(drawable);
    }

    public void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public void UnregisterUI(IDrawable drawable)
    {
        UIdrawables.Remove(drawable);
    }

    public void UnregisterHUD(IDrawable drawable)
    {
        HUDdrawables.Remove(drawable);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    public void DrawUI(SpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in UIdrawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    public void DrawHUD(SpriteBatch spriteBatch, float scale)
    {
        foreach (var drawable in HUDdrawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }
}