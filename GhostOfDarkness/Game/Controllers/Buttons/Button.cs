using System;
using System.Collections.Generic;
using Core.Extensions;
using game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = game.IDrawable;

namespace Game.Controllers.Buttons;

public class Button : IComponent
{
    private readonly float buttonLayer;
    private readonly List<IDrawable> drawables;
    protected readonly Texture2D Texture;
    protected readonly Vector2 Position;
    protected readonly Color SelectedColor;
    protected float Scale;

    public bool Selected { get; set; }
    public bool Active { get; set; } = true;

    public event Action OnClicked;

    public Button(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        buttonLayer = Layers.UI;
        SelectedColor = Color.LightGray;
        drawables = new List<IDrawable>();
    }

    public Button(Texture2D texture, Vector2 position, Color selectedColor) : this(texture, position)
    {
        SelectedColor = selectedColor;
    }

    public Button(Texture2D texture, Vector2 position, float buttonLayer) : this(texture, position)
    {
        this.buttonLayer = buttonLayer;
    }

    public Button(Texture2D texture, Vector2 position, float buttonLayer, Color selectedColor) : this(texture, position, selectedColor)
    {
        this.buttonLayer = buttonLayer;
    }

    public void AddDrawable(IDrawable drawable)
    {
        drawables.Add(drawable);
    }

    public void Update(float deltaTime)
    {
        if (!Active)
        {
            return;
        }

        Selected = InBounds(MouseController.WindowPosition);
        if (Selected && MouseController.LeftButtonClicked())
        {
            OnClicked?.Invoke();
        }
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        Scale = scale;
        var position = Position * scale;
        var defaultColor = Color.White;
        var color = Active
            ? Selected ? SelectedColor : defaultColor
            : defaultColor.WithAlpha(200).WithColorDelta(190);
        spriteBatch.Draw(Texture, position, null, color, 0, Vector2.Zero, scale, SpriteEffects.None, buttonLayer);

        foreach (var drawable in drawables)
        {
            drawable.Draw(spriteBatch, scale);
        }
    }

    private bool InBounds(Vector2 mousePosition)
    {
        var width = Texture.Width * Scale;
        var height = Texture.Height * Scale;
        var x = Position.X * Scale;
        var y = Position.Y * Scale;
        var bounds = new Rectangle((int)x, (int)y, (int)width, (int)height);
        return bounds.Contains(mousePosition);
    }
}