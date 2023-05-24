using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace game;

internal class QuadTree : IDrawable
{
    private static int checkCount; // for tests
    private static int depth; // for tests
    private readonly static int threshold = 4;

    private readonly Rectangle boundary;
    private readonly List<ICollisionable> items;
    private readonly QuadTree[] nodes;

    public static bool Show { get; set; }

    public int Count
    { 
        get
        {
            if (nodes[0] is null)
                return items.Count;
            return items.Count + nodes.Sum(x => x.Count);
        }
    }

    public QuadTree(Rectangle boundary)
    {
        this.boundary = boundary;
        items = new();
        nodes = new QuadTree[4];
    }

    public void Insert(ICollisionable item)
    {
        var hitbox = item.Hitbox.Shift(item.Position);
        if (!hitbox.Intersects(boundary))
            return;
        if (items.Count < threshold)
        {
            items.Add(item);
            return;
        }

        if (nodes[0] is null)
            Subdivide();

        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Insert(item);
        }
    }

    internal void Remove(ICollisionable item)
    {
        var hitbox = item.Hitbox.Shift(item.Position);
        if (!hitbox.Intersects(boundary) || items.Remove(item) || nodes[0] is null)
            return;

        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Remove(item);
        }

        if (nodes.All(x => x.Count == 0))
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = null;
            }
        }
    }

    public bool IsIntersectedWithItems(ICollisionable value, Vector2 movementVector)
    {
        checkCount = 0;
        depth = 0;
        return GetIntersectWithItems(value, movementVector) is not null;
    }

    public ICollisionable GetIntersectWithItems(ICollisionable value)
    {
        checkCount = 0;
        depth = 0;
        return GetIntersectWithItems(value, Vector2.Zero);
    }

    public ICollisionable GetIntersectWithItems(ICollisionable value, Vector2 movementVector)
    {
        depth++;
        var hitbox = value.Hitbox.Shift(value.Position + movementVector);
        if (!hitbox.Intersects(boundary))
            return null;

        for (int i = 0; i < items.Count; i++)
        {
            checkCount++;
            if (value != items[i] && items[i].CanCollide && value.Collision(items[i], movementVector))
                return items[i];
        }
        if (nodes[0] is not null)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                var intersect = nodes[i].GetIntersectWithItems(value, movementVector);
                if (intersect is not null)
                    return intersect;
            }
        }
        //Debug.Log($"{value} count: {checkCount}  depth: {depth}");
        return null;
    }

    private void Subdivide()
    {
        var quarter = boundary.Quarter();
        nodes[0] = new(quarter);
        quarter.Offset(quarter.Width, 0);
        nodes[1] = new(quarter);
        quarter.Offset(0, quarter.Height);
        nodes[2] = new(quarter);
        quarter.Offset(-quarter.Width, 0);
        nodes[3] = new(quarter);
    }

    public void Draw(SpriteBatch spriteBatch, float scale)
    {
        if (!Show)
            return;
        var vertical = TexturesManager.VerticalLine;
        var horizontal = TexturesManager.HorizontalLine;
        var position = boundary.Location.ToVector2();
        var source = new Rectangle(0, 0, 2, boundary.Height);
        spriteBatch.Draw(vertical, position, source, Color.White, 0, new Vector2(1.5f, 0), 1, SpriteEffects.None, Layers.HUD);
        source = new Rectangle(0, 0, boundary.Width, 2);
        spriteBatch.Draw(horizontal, position, source, Color.White, 0, new Vector2(0, 1.5f), 1, SpriteEffects.None, Layers.HUD);
        position.X += boundary.Width;
        source = new Rectangle(0, 0, 2, boundary.Height);
        spriteBatch.Draw(vertical, position, source, Color.White, 0, new Vector2(1.5f, 0), 1, SpriteEffects.None, Layers.HUD);
        position.X -= boundary.Width;
        position.Y += boundary.Height;
        source = new Rectangle(0, 0, boundary.Width, 2);
        spriteBatch.Draw(horizontal, position, source, Color.White, 0, new Vector2(0, 1.5f), 1, SpriteEffects.None, Layers.HUD);

        if (nodes[0] is not null)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].Draw(spriteBatch, scale);
            }
        }
    }
}