using game.Extensions;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game.Structures;

internal class QuadTree
{
    private static int countItems = 4;

    private readonly Rectangle boundary;
    private List<Rectangle> items;

    private QuadTree topRight;
    private QuadTree topLeft;
    private QuadTree downLeft;
    private QuadTree downRight;

    public QuadTree(Rectangle boundary)
    {
        this.boundary = boundary;
    }

    public void Insert(Rectangle item)
    {
        if (!item.Intersects(boundary))
            return;
        if (items.Count < countItems)
        {
            items.Add(item);
            return;
        }

        if (topRight == null)
            Subdivide();

        topRight.Insert(item);
        topLeft.Insert(item);
        downLeft.Insert(item);
        downRight.Insert(item);
    }

    public void Subdivide()
    {
        var quarter = boundary.Quarter();
        topLeft = new(quarter);
        quarter.Offset(quarter.Width, 0);
        topRight = new(quarter);
        quarter.Offset(0, quarter.Height);
        downRight = new(quarter);
        quarter.Offset(-quarter.Width, 0);
        downLeft = new(quarter);
    }
}