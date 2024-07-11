using Core.Extensions;
using Microsoft.Xna.Framework;

namespace Game.Controllers;

public class StickyCursor
{
    private readonly Rectangle outerBounds;
    private readonly Point? indent;
    private Rectangle innerBounds;
    private Vector2 origin;
    private Vector2 offset;
    private float scale = 1;

    public bool IsStuck { get; private set; }
    public bool Selected { get; private set; }
    public Vector2 StickPosition { get; private set; }

    public StickyCursor(Rectangle outerBounds, Rectangle innerBounds, Point? indent)
    {
        origin = new Vector2(innerBounds.Width / 2f, innerBounds.Height / 2f);
        this.outerBounds = outerBounds;
        this.innerBounds = innerBounds.Shift(-origin);
        this.indent = indent;
    }

    public void Update()
    {
        var mousePosition = MouseController.WindowPosition / scale;
        if (MouseController.LeftButtonClicked() && MouseInBounds(mousePosition))
        {
            offset = mousePosition - innerBounds.Center.ToVector2();
            IsStuck = true;
        }

        if (MouseController.LeftButtonReleased())
        {
            IsStuck = false;
        }

        Selected = MouseInBounds(mousePosition) || IsStuck;
        if (IsStuck)
        {
            StickPosition = outerBounds.GetVectorInBounds(mousePosition - offset, indent);
            innerBounds.Location = StickPosition.ToPoint() - origin.ToPoint();
        }
    }

    public void SetScale(float newScale)
    {
        scale = newScale;
    }

    private bool MouseInBounds(Vector2 mousePosition) => innerBounds.Contains(mousePosition);
}