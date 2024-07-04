using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Game.Extensions;

public static class PathExtension
{
    public static List<Vector2> ToMovementVectors(this IEnumerable<Point> value)
    {
        var path = value.ToList();
        path.Reverse();
        var previousPoint = path.First();
        return path.Skip(1).Select(x =>
        {
            var offset = x - previousPoint;
            previousPoint = x;
            return offset.ToVector2();
        }).ToList();
    }
}