using game.Creatures;
using game.Interfaces;
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
            if (item.Collision(obj))
            {
                return obj;
            }
        }
        return null;
    }
}