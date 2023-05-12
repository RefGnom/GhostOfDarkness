using game.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game.Managers;

internal class CollisionDetecter
{
    private List<ICollisionable> objects = new();

    public void Register(ICollisionable item)
    {
        objects.Add(item);
    }

    public void Unregister(ICollisionable item)
    {
        objects.Remove(item);
    }

    public ICollisionable CollisionWithbjects(ICollisionable item)
    {
        foreach (var obj in objects)
        {
            if (obj == item)
                continue;
            if (item.Collision(obj))
            {
                return obj;
            }
        }
        return null;
    }

    public bool CollisionWithbjects(ICollisionable item, Vector2 moveVector)
    {
        foreach (var obj in objects)
        {
            if (obj == item)
                continue;
            if (item.Collision(obj, moveVector))
            {
                return true;
            }
        }
        return false;
    }
}