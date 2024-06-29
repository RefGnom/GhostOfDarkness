using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Game.Extensions;

namespace game;

internal static class RectangleExtension
{
    public static IEnumerable<(Rectangle, float)> GetNeighbors(this Rectangle rectangle, int shift)
    {
        foreach (var (neighbour, cost) in new Point(0, 0).GetNeighbors())
        {
            var movementVector = neighbour.ToVector2();
            var (x, y) = movementVector.ToPoint();
            yield return (rectangle.Shift(x * shift, y * shift), cost);
        }
    }

    public static float Distance(this Rectangle rectangle1, Rectangle rectangle2)
    {
        return rectangle1.Center.CalculateDistanceByPixels(rectangle2.Center);
    }

    public static float Radius(this Rectangle rectangle)
    {
        var w = rectangle.Width;
        var h = rectangle.Height;
        return MathF.Sqrt(w * w + h * h) / 2;
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