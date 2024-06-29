using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game.Model;

namespace game;

internal static class PathFinder
{
    public static Path<Rectangle> GetMovementVector(Room room, Rectangle hitbox, Rectangle target, int maxDistance)
    {
        var path = new Path<Rectangle>(hitbox);
        var paths = new Queue<Path<Rectangle>>();
        var visited = new HashSet<Rectangle>();

        paths.Enqueue(path);
        visited.Add(hitbox);

        while (paths.Count != 0)
        {
            var currentPath = paths.Dequeue();
            var currentPosition = currentPath.Value;
            foreach (var (nextPosition, cost) in currentPosition.GetNeighbors(room.TileSize))
            {
                if (visited.Contains(nextPosition) || !room.IsPossiblePosition(nextPosition))
                    continue;

                var nextPath = new Path<Rectangle>(nextPosition, currentPath);
                if (Vector2.Distance(nextPosition.Center.ToVector2(), target.Center.ToVector2()) <= room.TileSize)
                    return nextPath;
                if (nextPath.Length <= maxDistance)
                    paths.Enqueue(nextPath);
                visited.Add(nextPosition);
            }
        }
        return null;
    }
}