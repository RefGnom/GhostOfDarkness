using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace game;

internal class Node
{
    public Point Point { get; private set; }
    public float Cost { get; private set; }
    public float Distance { get; private set; }
    public float TotalCost => Cost + Distance;
    public Node Previous { get; private set; }

    public Node(Point point, float cost, Node previous = null)
    {
        Point = point;
        Cost = cost;
        if (previous is not null)
            Cost += previous.Cost;
        Previous = previous;
    }

    public void CalculateDistance(Point end)
    {
        var distanceX = MathF.Abs(end.X - Point.X);
        var distanceY = MathF.Abs(end.Y - Point.Y);
        Distance = distanceX + distanceY;
    }
}

internal static class AStar
{
    public static List<Point> FindPath(Tile[,] tiles, Point start, Point end)
    {
        var forOpen = new PriorityQueue<Node, float>();
        var visited = new HashSet<Point>() { start };
        var startNode = new Node(start, 0);
        forOpen.Enqueue(startNode, startNode.TotalCost);

        while (forOpen.Count > 0)
        {
            var node = forOpen.Dequeue();
            foreach (var (neighbour, cost) in GetNeighbors(node.Point))
            {
                if (visited.Contains(neighbour) || !IsPossiblePoint(neighbour, tiles))
                    continue;
                var nextNode = new Node(neighbour, cost, node);
                nextNode.CalculateDistance(end);

                if (neighbour == end)
                    return GetPath(nextNode);

                forOpen.Enqueue(nextNode, nextNode.TotalCost);
                visited.Add(neighbour);
            }
        }
        return null;
    }

    private static List<Point> GetPath(Node node)
    {
        var result = new List<Point>();
        while (node is not null)
        {
            result.Add(node.Point);
            node = node.Previous;
        }
        result.Reverse();
        return result;
    }

    private static bool IsPossiblePoint(Point point, Tile[,] tiles)
    {
        // Стоить попробовать убрать проверку, что объект не ICollisionable и посмотреть как это скажется на скорости
        return point.X >= 0 && point.X < tiles.GetLength(0)
            && point.Y >= 0 && point.Y < tiles.GetLength(1)
            && tiles[point.X, point.Y] is not ICollisionable;
    }

    private static IEnumerable<(Point, float)> GetNeighbors(Point point)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                var neighbour = new Point(point.X + dx, point.Y + dy);
                var cost = dx == 0 || dy == 0 ? 1 : MathF.Sqrt(2);
                yield return (neighbour, cost);
            }
        }
    }
}