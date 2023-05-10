using Microsoft.Xna.Framework;

namespace game.Extensions;

internal static class RectangleExtension
{
    public static Rectangle Quarter(this Rectangle rectangle)
    {
        var size = new Point(rectangle.Width / 2, rectangle.Height / 2);
        return new Rectangle(rectangle.Location, size);
    }

    public static Rectangle Shift(this Rectangle rectangle, Vector2 amount)
    {
        rectangle.Offset(amount);
        return rectangle;
    }
}