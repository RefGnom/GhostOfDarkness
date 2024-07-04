using System;
using System.Collections.Generic;
using Core.DependencyInjection;
using Core.Extensions;
using Game.Graphics;
using Game.Interfaces;
using Microsoft.Xna.Framework;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Controllers.Buttons;

[DiIgnore]
public abstract class BaseButton : IComponent
{
    private readonly List<IDrawable> drawables = [];
    protected Vector2 Position;
    protected float Scale;
    protected float Layer;

    public bool Selected { get; set; }
    public bool Inactive { get; set; }

    public event Action OnClicked;

    protected BaseButton(Vector2 position, float layer)
    {
        Position = position;
        Layer = layer;
    }

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

    public void Draw(ISpriteBatch spriteBatch, float scale)
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

    protected virtual void DrawButton(ISpriteBatch spriteBatch, float scale)
    {
    }

    private bool InBounds(Vector2 mousePosition) => GetBounds().Scale(Scale).Contains(mousePosition);

    protected abstract Rectangle GetBounds();
}