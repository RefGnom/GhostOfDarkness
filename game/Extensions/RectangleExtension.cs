using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game;

internal static class RectangleExtension
{
    public static List<Rectangle> GetMoves(this Rectangle rectangle, int speedX, int speedY)
    {
        var result = new List<Rectangle>();
        foreach (var direction in new Point(0, 0).GetNeighbors())
        {
            var movementVector = direction.ToVector2();
            var (x, y) = movementVector.ToPoint();
            result.Add(rectangle.Shift(x * speedX, y * speedY));
        }
        return result;
    }

    public static Vector2 GetOffset(this Rectangle rectangle1, Rectangle rectangle2)
    {
        var x = rectangle1.X - rectangle2.X;
        var y = rectangle1.Y - rectangle2.Y;
        return new Vector2(x, y);
    }

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

    public static Rectangle Shift(this Rectangle rectangle, int x, int y)
    {
        rectangle.Offset(x, y);
        return rectangle;
    }
}