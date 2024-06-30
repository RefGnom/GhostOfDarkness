using System;
using System.Collections.Generic;
using Core.Extensions;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = game.IDrawable;

namespace Game.Controllers.Buttons;

public abstract class BaseButton : IComponent
{
    private readonly List<IDrawable> drawables = new List<IDrawable>();
    protected Vector2 Position;
    protected float Scale;

    public bool Selected { get; set; }
    public bool Inactive { get; set; }

    public event Action OnClicked;

    public void AddDrawable(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void Update(float deltaTime)
    {
        if (Inactive)
        {
            return;
        }

        Selected = InBounds(MouseController.WindowPosition);
        if (Selected && MouseController.LeftButtonClicked())
        {
            OnClicked?.Invoke();
        }

        UpdateButton(deltaTime);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        Scale = scale;
        DrawButton(spriteBatch, scale);
        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    protected virtual void UpdateButton(float deltaTime)
    {
    }

    protected virtual void DrawButton(SpriteBatch spriteBatch, float scale)
    {
    }

    private bool InBounds(Vector2 mousePosition) => GetBounds().Scale(Scale).Contains(mousePosition);

    protected abstract Rectangle GetBounds();
}