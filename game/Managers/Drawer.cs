﻿using game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game.Managers;

internal class Drawer
{
    private List<IDrawable> drawables = new();
    private List<IDrawable> UIdrawables = new();

    public void Register(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void RegisterUI(IDrawable drawable)
    {
        UIdrawables.Add(drawable);
    }

    public void Unregister(IDrawable drawable)
    {
        drawables.Remove(drawable);
    }

    public void UnregisterUI(IDrawable drawable)
    {
        UIdrawables.Remove(drawable);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch);
        }
    }

    public void DrawUI(SpriteBatch spriteBatch)
    {
        foreach (var drawable in UIdrawables)
        {
            drawable.Draw(spriteBatch);
        }
    }
}