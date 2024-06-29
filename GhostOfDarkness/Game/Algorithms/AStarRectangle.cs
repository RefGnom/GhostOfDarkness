using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Game.Extensions;
using Game.Model;

namespace game;

internal static class AStarRectangle
{
    public static List<Rectangle> FindPath(Room room, Rectangle start, Rectangle end, float maxCost)
    {
        if (start.Distance(end) <= start.Radius() + end.Radius())
            return new List<Rectangle>();

        var forOpen = new PriorityQueue<PathNode<Rectangle>, float>();
        var visited = new HashSet<Rectangle>() { start };
        var startPoint = room.ConvertToCoordinatePoint(start, x => (int)x);
        var endPoint = room.ConvertToCoordinatePoint(end, x => (int)x);
        var startNode = new PathNode<Rectangle>(start, 0, startPoint.CalculateDistanceByPixels(endPoint));
        forOpen.Enqueue(startNode, startNode.TotalCost);

        while (forOpen.Count > 0)
        {
            var node = forOpen.Dequeue();
            var value = node.Value;
            if (node.TotalCost > maxCost)
                return null;
            foreach (var (neighbour, cost) in value.GetNeighbors(room.TileSize))
            {
                var roomPoint = room.ConvertToCoordinatePoint(neighbour, x => (int)x);
                if (visited.Contains(neighbour) || !room.IsPossiblePosition(neighbour))
                    continue;
                var nextNode = new PathNode<Rectangle>(neighbour, cost, roomPoint.CalculateDistanceByPixels(endPoint), node);
                if (neighbour.Distance(end) <= neighbour.Radius() + end.Radius())
                    return nextNode.GetPath();

                forOpen.Enqueue(nextNode, nextNode.TotalCost);
                visited.Add(neighbour);
            }
        }
        return null;
    }
}