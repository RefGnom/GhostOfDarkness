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

    public StickyCursor(IMouseService mouseService, Rectangle outerBounds, Rectangle innerBounds, Point? indent = null)
    {
        this.mouseService = mouseService;
        this.outerBounds = outerBounds;
        InnerBounds = innerBounds.Shift(indent ?? Point.Zero);
        innerBoundsPosition = InnerBounds.Location.ToVector2();
        this.indent = indent;
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
            innerBoundsPosition = new Rectangle(outerBounds.Location, outerBounds.Size - InnerBounds.Size).GetVectorInBounds(innerBoundsPosition.Shift(positionDelta), indent);
            InnerBounds = new Rectangle(innerBoundsPosition.ToPoint(), InnerBounds.Size);
            //InnerBounds = outerBounds.GetRectangleInBounds(newInnerBounds, indent);
            attachPosition = mousePosition;
        }
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
    }

    private bool MouseInBounds(Vector2 mousePosition) => InnerBounds.Contains(mousePosition);
}