using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace game;

internal static class PathFinder
{
    private static bool IsPossiblePoint(int x, int y, Tile[,] location)
    {
        return InBounds(x, y, location) && location[x, y].Entity is not ICollisionable;
    }

    private static bool IsPossiblePosition(Rectangle hitbox, Tile[,] location)
    {
        var tile = location[0, 0];
        var topLeft = (hitbox.Location.ToVector2() - tile.Position) / tile.Size;
        topLeft.Floor();
        var bottomRight = (new Vector2(hitbox.Right, hitbox.Bottom) - tile.Position) / tile.Size;
        bottomRight.Ceiling();
        var (x1, y1) = topLeft.ToPoint();
        var (x2, y2) = bottomRight.ToPoint();

        return IsPossiblePoint(x1, y1, location)
            && IsPossiblePoint(x1, y2, location)
            && IsPossiblePoint(x2, y1, location)
            && IsPossiblePoint(x2, y2, location);
    }

    public static Path<Rectangle> GetMovementVector(Tile[,] location, Rectangle hitbox, Rectangle target, int maxDistance)
    {
        var count = 0;
        var successCount = 0;
        var tile = location[0, 0];
        var path = new Path<Rectangle>(hitbox);
        var paths = new Queue<Path<Rectangle>>();
        var visited = new HashSet<Rectangle>();

        var speedX = hitbox.Width;
        var speedY = hitbox.Height;

        paths.Enqueue(path);
        visited.Add(hitbox);

        while (paths.Count != 0)
        {
            var currentPath = paths.Dequeue();
            var currentPosition = currentPath.Value;
            foreach (var nextPosition in currentPosition.GetMoves(speedX, speedY))
            {
                count++;
                if (visited.Contains(nextPosition) || !IsPossiblePosition(nextPosition, location))
                    continue;
                successCount++;

                var nextPath = new Path<Rectangle>(nextPosition, currentPath);
                if (Vector2.Distance(nextPosition.Center.ToVector2(), target.Center.ToVector2()) <= tile.Size)
                    return nextPath;
                if (nextPath.Length <= maxDistance)
                    paths.Enqueue(nextPath);
                visited.Add(nextPosition);
            }
        }
        Debug.Log($"{count} {successCount}");
        return null;
    }

    public static Path<Point> GetPath(Tile[,] location, Vector2 position, Vector2 target, int maxDistance)
    {
        var delta = location[0, 0].Position;
        var start = ConvertToCoordinatePoint(position - delta, location[0, 0].Size);
        var end = ConvertToCoordinatePoint(target - delta, location[0, 0].Size);

        var path = new Path<Point>(start);
        var nextTiles = new Queue<Path<Point>>();
        var visited = new HashSet<Point>();

        nextTiles.Enqueue(path);

        while (nextTiles.Count != 0)
        {
            var currentPath = nextTiles.Dequeue();
            foreach (var nextPoint in currentPath.Value.GetNeighbors())
            {
                if (!IsPossible(nextPoint, location) || visited.Contains(nextPoint))
                    continue;
                var nextPath = new Path<Point>(nextPoint, currentPath);
                if (nextPoint == end)
                    return nextPath;
                if (nextPath.Length <= maxDistance)
                    nextTiles.Enqueue(nextPath);
                visited.Add(nextPoint);
            }
        }
        return null;
    }

    private static bool IsPossible(Point point, Tile[,] tiles)
    {
        return InBounds(point, tiles) && tiles[point.X, point.Y].Entity is not ICollisionable;
    }

    private static bool InBounds(Point point, Tile[,] tiles)
    {
        return InBounds(point.X, point.Y, tiles);
    }

    private static bool InBounds(int x, int y, Tile[,] tiles)
    {
        return x >= 0 && x < tiles.GetLength(0) 
            && y >= 0 && y < tiles.GetLength(1);
    }

    private static Point ConvertToCoordinatePoint(Vector2 vector, int tileSize)
    {
        return new Point((int)Math.Floor(vector.X / tileSize), (int)Math.Floor(vector.Y / tileSize));
    }
}