using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game.Extensions;

internal static class PointExtension
{
    public static IEnumerable<(Point, float)> GetNeighbors(this Point point)
    {
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }

                var neighbour = new Point(point.X + dx, point.Y + dy);
                var cost = dx == 0 || dy == 0 ? 1 : MathF.Sqrt(2);
                yield return (neighbour, cost);
            }
        }
    }

    public static float CalculateDistanceByTiles(this Point start, Point end)
    {
        var distanceX = MathF.Abs(end.X - start.X);
        var distanceY = MathF.Abs(end.Y - start.Y);
        return distanceX + distanceY;
    }

    public static float CalculateDistanceByPixels(this Point start, Point end) => Vector2.Distance(start.ToVector2(), end.ToVector2());
}