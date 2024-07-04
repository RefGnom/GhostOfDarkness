using System;
using Core.DependencyInjection;

namespace Game.Objects;

[DiIgnore]
internal class LoopedUnorderedQueue<T>
{
    private readonly T[] items;
    private int currentIndex;

    public LoopedUnorderedQueue(params T[] items)
    {
        this.items = items;
        Shuffle();
    }

    public T GetNext()
    {
        if (currentIndex == items.Length - 1)
        {
            Shuffle();
            currentIndex = 0;
        }
        return items[currentIndex++];
    }

    public LoopedUnorderedQueue<T> Shuffle()
    {
        var n = items.Length;
        var random = new Random();
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (items[n], items[k]) = (items[k], items[n]);
        }
        return this;
    }
}