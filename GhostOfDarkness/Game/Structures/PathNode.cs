using System.Collections.Generic;

namespace Game.Structures;

internal class PathNode<T>
{
    public readonly T Value;
    private readonly float cost;
    private readonly float distance;
    private readonly PathNode<T> previous;

    public float TotalCost => cost + distance;
    public int Length { get; private set; }

    public PathNode(T value, float cost, float distance, PathNode<T> previous = null)
    {
        Value = value;
        this.cost = cost;
        if (previous is not null)
        {
            this.cost += previous.cost;
            Length = previous.Length + 1;
        }
        else
        {
            Length = 1;
        }
        this.previous = previous;
        this.distance = distance;
    }

    public List<T> GetPath()
    {
        var result = new List<T>();
        var node = this;
        while (node is not null)
        {
            result.Add(node.Value);
            node = node.previous;
        }
        result.Reverse();
        return result;
    }
}