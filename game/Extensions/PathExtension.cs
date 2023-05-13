using game.Structures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace game.Extensions;

internal static class PathExtension
{
    public static List<Vector2> ToMovementVetors(this Path<Point> value)
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