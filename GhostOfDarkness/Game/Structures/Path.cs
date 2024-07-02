using System.Collections;
using System.Collections.Generic;

namespace Game.Structures;

internal class Path<T> : IEnumerable<T>
{
    public readonly T Value;
    public readonly Path<T> Previous;
    public readonly int Length;

    public Path(T value, Path<T> previous = null)
    {
        Value = value;
        Previous = previous;
        Length = previous?.Length + 1 ?? 1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
        var pathItem = Previous;
        while (pathItem != null)
        {
            yield return pathItem.Value;
            pathItem = pathItem.Previous;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}