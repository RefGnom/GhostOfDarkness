﻿using Core.Extensions;
using Game.Controllers.InputServices;
using Microsoft.Xna.Framework;

namespace Game.Controllers;

public class StickyCursor
{
    private readonly IMouseService mouseService;
    private readonly Rectangle outerBounds;
    private readonly Point? indent;
    private Vector2 offset;
    private float scale = 1;

    public bool IsStuck { get; private set; }
    public bool Selected { get; private set; }
    public Rectangle InnerBounds { get; private set; }

    public StickyCursor(IMouseService mouseService, Rectangle outerBounds, Rectangle innerBounds, Point? indent = null)
    {
        this.mouseService = mouseService;
        this.outerBounds = outerBounds;
        var origin = new Vector2(innerBounds.Width / 2f, innerBounds.Height / 2f);
        InnerBounds = innerBounds.Shift(-origin);
        this.indent = indent;
    }

    public void Update()
    {
        var mousePosition = mouseService.GetWindowPosition() / scale;
        if (mouseService.LeftButtonClicked() && MouseInBounds(mousePosition))
        {
            offset = mousePosition - InnerBounds.Center.ToVector2();
            IsStuck = true;
        }

        if (mouseService.LeftButtonReleased())
        {
            IsStuck = false;
        }

        Selected = MouseInBounds(mousePosition) || IsStuck;
        if (IsStuck)
        {
            var position = mousePosition - offset;
            var rectangle = new Rectangle(position.ToPoint(), InnerBounds.Size);
            InnerBounds = outerBounds.GetRectangleInBounds(rectangle, indent);
        }
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
    }

    private bool MouseInBounds(Vector2 mousePosition) => InnerBounds.Contains(mousePosition);
}