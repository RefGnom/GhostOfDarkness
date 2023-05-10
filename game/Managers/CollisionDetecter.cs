using game.Creatures;
using game.Interfaces;
using System.Collections.Generic;

namespace game.Managers;

internal class CollisionDetecter
{
    private List<ICollisionable> items;

    public void Register(ICollisionable item)
    {
        items.Add(item);
    }

    public void Unregister(ICollisionable item)
    {
        items.Remove(item);
    }

    public Creature CollisionWithEnemies(ICollisionable item)
    {
        foreach (var enemy in GameManager.Instance.EnemiesManager.Enemies)
        {
            if (item.Collision(enemy))
            {
                return enemy;
            }
        }
        return null;
    }
}