using Microsoft.Xna.Framework;

namespace Core.Extensions;

public static class RectangleExtension
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
        => rectangle1.Center.CalculateDistanceByPixels(rectangle2.Center);

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

    public static Rectangle Shift(this Rectangle rectangle, Point amount)
    {
        rectangle.Offset(amount);
        return rectangle;
    }

    public static Rectangle Shift(this Rectangle rectangle, int x, int y)
    {
        rectangle.Offset(x, y);
        return rectangle;
    }

    public static Rectangle Scale(this Rectangle rectangle, float scale)
    {
        var x = rectangle.X * scale;
        var y = rectangle.Y * scale;
        var width = rectangle.Width * scale;
        var height = rectangle.Height * scale;
        return new Rectangle((int)x, (int)y, (int)width, (int)height);
    }

    public static Vector2 GetVectorInBounds(this Rectangle bounds, Vector2 vector, Point? indent = null)
    {
        indent ??= Point.Zero;

        var location = bounds.Location + indent.Value;
        var size = bounds.Size - indent.Value * new Point(2, 2);
        var boundsWithIndent = new Rectangle(location, size);

        if (vector.X < boundsWithIndent.Left)
        {
            vector.X = boundsWithIndent.Left;
        }

        if (vector.X > boundsWithIndent.Right)
        {
            vector.X = boundsWithIndent.Right;
        }

        if (vector.Y < boundsWithIndent.Top)
        {
            vector.Y = boundsWithIndent.Top;
        }

        if (vector.Y > boundsWithIndent.Bottom)
        {
            vector.Y = boundsWithIndent.Bottom;
        }

        return vector;
    }

    public static Rectangle GetRectangleInBounds(this Rectangle bounds, Rectangle rectangle, Point? indent = null)
    {
        indent ??= Point.Zero;

        var location = bounds.Location + indent.Value;
        var size = bounds.Size - indent.Value * new Point(2, 2);
        var boundsWithIndent = new Rectangle(location, size);

        if (rectangle.Left < boundsWithIndent.Left)
        {
            rectangle.X = boundsWithIndent.Left;
        }

        if (rectangle.Right > boundsWithIndent.Right)
        {
            rectangle.X = boundsWithIndent.Right - rectangle.Width;
        }

        if (rectangle.Top < boundsWithIndent.Top)
        {
            rectangle.Y = boundsWithIndent.Top;
        }

        if (rectangle.Bottom > boundsWithIndent.Bottom)
        {
            rectangle.Y = boundsWithIndent.Bottom - rectangle.Height;
        }

        return rectangle;
    }
}