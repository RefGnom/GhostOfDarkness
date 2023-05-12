using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game.Extensions;

internal static class PointExtension
{
    private static readonly int[] offsets = new int[] { 1, 0, -1 };

    public static IEnumerable<Point> GetNeighbors(this Point point)
    {
        foreach (var deltaX in offsets)
        {
            foreach (var deltaY in offsets)
            {
                if (deltaX == 0 && deltaY == 0)
                    continue;
                yield return point + new Point(deltaX, deltaY);
            }
        }
    }
}