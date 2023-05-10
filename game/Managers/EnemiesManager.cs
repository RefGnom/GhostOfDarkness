using game.Creatures;
using System.Collections.Generic;

namespace game.Managers;

internal class EnemiesManager
{
    private List<Creature> enemies = new();

    public List<Creature> Enemies => enemies;

    public void Add(Creature creature)
    {
        enemies.Add(creature);
    }

    public void Remove(Creature creature)
    {
        enemies.Remove(creature);
    }
}