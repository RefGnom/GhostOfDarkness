using Microsoft.Xna.Framework;

namespace Game.Controllers;

public class StickyCursor
{
    private Rectangle bounds;

    public bool IsStuck { get; private set; }
    public bool Selected { get; private set; }

    public StickyCursor(Rectangle bounds)
    {
        this.bounds = bounds;
    }

    public void SetBounds(Rectangle newBounds)
    {
        bounds = newBounds;
    }

    public void Update(float scale)
    {
        if (MouseController.LeftButtonClicked() && MouseInBounds())
        {
            IsStuck = true;
        }

        if (MouseController.LeftButtonReleased())
        {
            IsStuck = false;
        }

        Selected = MouseInBounds() || IsStuck;
    }
    private bool MouseInBounds() => bounds.Contains(MouseController.WindowPosition);
}