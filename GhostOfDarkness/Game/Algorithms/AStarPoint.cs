using System.Collections.Generic;
using Core.Extensions;
using Game.Model;
using Game.Structures;
using Microsoft.Xna.Framework;

namespace Game.Algorithms;

public static class AStarPoint
{
    public static List<Point> FindPath(Room room, Point start, Point end)
    {
        var forOpen = new PriorityQueue<PathNode<Point>, float>();
        var visited = new HashSet<Point>() { start };
        var startNode = new PathNode<Point>(start, 0, start.CalculateDistanceByPixels(end));
        forOpen.Enqueue(startNode, startNode.TotalCost);

        while (forOpen.Count > 0)
        {
            var node = forOpen.Dequeue();
            foreach (var (neighbour, cost) in node.Value.GetNeighbors())
            {
                if (visited.Contains(neighbour) || !room.IsPossiblePosition(neighbour))
                {
                    continue;
                }

                var nextNode = new PathNode<Point>(neighbour, cost, neighbour.CalculateDistanceByPixels(end), node);

                if (neighbour == end)
                {
                    return nextNode.GetPath();
                }

                forOpen.Enqueue(nextNode, nextNode.TotalCost);
                visited.Add(neighbour);
            }
        }
        return null;
    }
}