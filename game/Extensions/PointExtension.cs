using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace game;

internal static class PointExtension
{
    public static IEnumerable<Point> GetNeighbors(this Point point)
    {
        for (int deltaX = -1; deltaX <= 1; deltaX++)
        {
            for (int deltaY = -1; deltaY <= 1; deltaY++)
            {
                if (Math.Abs(deltaX + deltaY) == 1)
                    yield return point + new Point(deltaX, deltaY);
            }
        }
        for (int deltaX = -1; deltaX <= 1; deltaX++)
        {
            for (int deltaY = -1; deltaY <= 1; deltaY++)
            {
                if (deltaX != 0 && deltaY != 0)
                    yield return point + new Point(deltaX, deltaY);
            }
        }
    }
}