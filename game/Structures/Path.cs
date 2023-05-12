using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace game.Structures;

internal class Path : IEnumerable<Vector2>
{
    private List<Vector2> movementVectors = new();

    public int Count => movementVectors.Count;

    public Vector2 this[int index]
    {
        get
        {
            if (index < 0 || index >= movementVectors.Count)
                throw new IndexOutOfRangeException();
            return movementVectors[index];
        }
        set
        {
            if (index < 0 || index >= movementVectors.Count)
                throw new IndexOutOfRangeException();
            movementVectors[index] = value;
        }
    }

    public void Add(Vector2 value) =>
        movementVectors.Add(value);

    public void Remove(Vector2 value) =>
        movementVectors.Remove(value);

    public IEnumerator<Vector2> GetEnumerator()
    {
        foreach (var value in movementVectors)
        {
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}