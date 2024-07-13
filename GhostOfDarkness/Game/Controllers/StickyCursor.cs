using System;
using Core.Extensions;
using Game.Controllers.InputServices;
using Microsoft.Xna.Framework;

namespace Game.Controllers;

public class StickyCursor
{
    private readonly IMouseService mouseService;
    private readonly Rectangle outerBounds;
    private readonly Point? indent;
    private Vector2 attachPosition;
    private Vector2 innerBoundsPosition;
    private float scale = 1;

    public bool IsStuck { get; private set; }
    public bool Selected { get; private set; }
    public Rectangle InnerBounds { get; private set; }

    public Action<Rectangle> InnerBoundsChanged;

    public StickyCursor(IMouseService mouseService, Rectangle outerBounds, Rectangle innerBounds, Point? indent = null)
    {
        this.mouseService = mouseService;
        this.outerBounds = outerBounds;
        InnerBounds = innerBounds.Shift(indent ?? Point.Zero);
        innerBoundsPosition = InnerBounds.Location.ToVector2();
        this.indent = indent;
    }

    public void ShiftInnerBoundsVertical(float shiftValue)
    {
        if (shiftValue < -1 || shiftValue > 1)
        {
            throw new ArgumentException("Shift value must be between -1 and 1");
        }

        var totalVerticalPixels = outerBounds.Height - InnerBounds.Height;
        var pixelsToShift = totalVerticalPixels * -shiftValue;
        var shiftedInnerBounds = InnerBounds.Shift(0, (int)pixelsToShift);
        InnerBounds = outerBounds.GetRectangleInBounds(shiftedInnerBounds, indent);
        InnerBoundsChanged(InnerBounds);
    }

    public void Update()
    {
        var mousePosition = mouseService.GetWindowPosition() / scale;
        if (mouseService.LeftButtonClicked() && MouseInBounds(mousePosition))
        {
            attachPosition = mousePosition;
            IsStuck = true;
        }

        if (mouseService.LeftButtonReleased())
        {
            IsStuck = false;
        }

        Selected = MouseInBounds(mousePosition) || IsStuck;
        if (IsStuck)
        {
            var positionDelta = mousePosition - attachPosition;
            innerBoundsPosition = outerBounds.Subtract(InnerBounds, outerBounds.Location)
                .GetVectorInBounds(innerBoundsPosition.Shift(positionDelta), indent);
            InnerBounds = InnerBounds.WithLocation(innerBoundsPosition);
            InnerBoundsChanged(InnerBounds);
            attachPosition = mousePosition;
        }
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
    }

    private bool MouseInBounds(Vector2 mousePosition) => InnerBounds.Contains(mousePosition);
}