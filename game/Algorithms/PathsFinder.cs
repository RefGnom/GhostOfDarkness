using game.Model;
using game.Structures;
using game.Extensions;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace game.Algorithms;

internal static class PathsFinder
{
    public static Path<Point> GetPathToPlayer(Tile[,] location, Vector2 position, Vector2 playerPosition)
    {
        var start = ConvertToCoordinatePoint(position, location[0, 0].Size);
        var end = ConvertToCoordinatePoint(playerPosition, location[0, 0].Size);

        var path = new Path<Point>(start);
        var nextTiles = new Queue<Path<Point>>();
        var visited = new HashSet<Point>();

        nextTiles.Enqueue(path);

        while (nextTiles.Count != 0)
        {
            var currentPath = nextTiles.Dequeue();
            foreach (var nextPoint in currentPath.Value.GetNeighbors())
            {
                if (location[nextPoint.X, nextPoint.Y].Entity is null
                    && !visited.Contains(nextPoint))
                {
                    var nextPath = new Path<Point>(nextPoint, currentPath);
                    if (nextPoint == end)
                        return nextPath;
                    nextTiles.Enqueue(nextPath);
                    visited.Add(nextPoint);
                }
            }
        }
        return path;
    }

    private static Point ConvertToCoordinatePoint(Vector2 vector, int tileSize)
    {
        return new Point((int)Math.Ceiling(vector.X / tileSize), (int)Math.Ceiling(vector.Y / tileSize));
    }
}