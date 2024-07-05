using System.Collections.Generic;
using System.Linq;
using Core.Extensions;
using Game.ContentLoaders;
using Game.Graphics;
using Game.Interfaces;
using Game.Service;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Game.Interfaces.IDrawable;

namespace Game.Structures;

public class QuadTree : IDrawable
{
    private const int threshold = 4;

    private readonly Rectangle boundary;
    private readonly List<ICollisionable> items;
    private readonly QuadTree[] nodes;

    public static bool Show { get; set; }
    public bool Divided => nodes[0] is not null;

    public int Count
    {
        get
        {
            if (nodes[0] is null)
            {
                return items.Count;
            }

            return items.Count + nodes.Sum(x => x.Count);
        }
    }

    public QuadTree(Rectangle boundary)
    {
        this.boundary = boundary;
        items = [];
        nodes = new QuadTree[4];
    }

    public void Insert(ICollisionable item)
    {
        var hitbox = item.Hitbox.Shift(item.Position);
        if (!hitbox.Intersects(boundary))
        {
            return;
        }

        if (items.Count < threshold)
        {
            items.Add(item);
            return;
        }

        if (nodes[0] is null)
        {
            Subdivide();
        }

        foreach (var t in nodes)
        {
            t.Insert(item);
        }
    }

    public void Remove(ICollisionable item)
    {
        var hitbox = item.Hitbox.Shift(item.Position);
        if (!hitbox.Intersects(boundary) || items.Remove(item) || nodes[0] is null)
        {
            return;
        }

        foreach (var t in nodes)
        {
            t.Remove(item);
        }

        if (nodes.All(x => x.Count == 0))
        {
            for (var i = 0; i < nodes.Length; i++)
            {
                nodes[i] = null;
            }
        }
    }

    public bool IsIntersectedWithItems(ICollisionable value) => GetIntersectWithItems(value, Vector2.Zero) is not null;

    public bool IsIntersectedWithItems(ICollisionable value, Vector2 movementVector) => GetIntersectWithItems(value, movementVector) is not null;

    public ICollisionable GetIntersectWithItems(ICollisionable value) => GetIntersectWithItems(value, Vector2.Zero);

    private ICollisionable GetIntersectWithItems(ICollisionable value, Vector2 movementVector)
    {
        if (!value.CanCollide)
        {
            return null;
        }

        var hitbox = value.Hitbox.Shift(value.Position + movementVector);
        if (!hitbox.Intersects(boundary))
        {
            return null;
        }

        foreach (var t in items)
        {
            if (value != t && t.CanCollide && value.Collision(t, movementVector))
            {
                return t;
            }
        }

        if (nodes[0] is not null)
        {
            foreach (var t in nodes)
            {
                var intersect = t.GetIntersectWithItems(value, movementVector);
                if (intersect is not null)
                {
                    return intersect;
                }
            }
        }

        return null;
    }

    private void Subdivide()
    {
        var quarter = boundary.Quarter();
        nodes[0] = new QuadTree(quarter);
        quarter.Offset(quarter.Width, 0);
        nodes[1] = new QuadTree(quarter);
        quarter.Offset(0, quarter.Height);
        nodes[2] = new QuadTree(quarter);
        quarter.Offset(-quarter.Width, 0);
        nodes[3] = new QuadTree(quarter);
    }

    public void Draw(ISpriteBatch spriteBatch, float scale)
    {
        if (!Show)
        {
            return;
        }

        var vertical = Textures.VerticalLine;
        var horizontal = Textures.HorizontalLine;
        var position = boundary.Location.ToVector2();
        var source = new Rectangle(0, 0, 2, boundary.Height);
        spriteBatch.Draw(vertical, position, source, Color.White, 0, new Vector2(1.5f, 0), 1, SpriteEffects.None, Layers.HudForeground);
        source = new Rectangle(0, 0, boundary.Width, 2);
        spriteBatch.Draw(horizontal, position, source, Color.White, 0, new Vector2(0, 1.5f), 1, SpriteEffects.None, Layers.HudForeground);
        position.X += boundary.Width;
        source = new Rectangle(0, 0, 2, boundary.Height);
        spriteBatch.Draw(vertical, position, source, Color.White, 0, new Vector2(1.5f, 0), 1, SpriteEffects.None, Layers.HudForeground);
        position.X -= boundary.Width;
        position.Y += boundary.Height;
        source = new Rectangle(0, 0, boundary.Width, 2);
        spriteBatch.Draw(horizontal, position, source, Color.White, 0, new Vector2(0, 1.5f), 1, SpriteEffects.None, Layers.HudForeground);

        if (nodes[0] is not null)
        {
            foreach (var t in nodes)
            {
                t.Draw(spriteBatch, scale);
            }
        }
    }
}