using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace game;

internal static class PathFinder
{
    public static Tile[,] Location { get; set; }

    public static Path<Point> GetPath(Vector2 position, Vector2 target, int maxDistance)
    {
        var start = ConvertToCoordinatePoint(position, Location[0, 0].Size);
        var end = ConvertToCoordinatePoint(target, Location[0, 0].Size);

        var path = new Path<Point>(start);
        var nextTiles = new Queue<Path<Point>>();
        var visited = new HashSet<Point>();

        nextTiles.Enqueue(path);

        while (nextTiles.Count != 0)
        {
            var currentPath = nextTiles.Dequeue();
            foreach (var nextPoint in currentPath.Value.GetNeighbors())
            {
                if (Location[nextPoint.X, nextPoint.Y].Entity is null
                    && !visited.Contains(nextPoint))
                {
                    var nextPath = new Path<Point>(nextPoint, currentPath);
                    if (nextPoint == end)
                        return nextPath;
                    if (nextPath.Length <= maxDistance)
                        nextTiles.Enqueue(nextPath);
                    visited.Add(nextPoint);
                }
            }
        }
        return null;
    }

    private static Point ConvertToCoordinatePoint(Vector2 vector, int tileSize)
    {
        return new Point((int)Math.Floor(vector.X / tileSize), (int)Math.Floor(vector.Y / tileSize));
    }
}