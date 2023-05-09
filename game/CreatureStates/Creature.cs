using Microsoft.Xna.Framework;

namespace game.CreatureStates;

public abstract class Creature
{
    public Vector2 Position { get; protected set; }
    public float Damage { get; protected set; }
    public float Health { get; protected set; }
    public float Speed { get; protected set; }

    public void Init(Vector2 position, float speed)
    {
        Position = position;
        Speed = speed;
        Health = 100;
        Damage = 10;
    }

    public void Init(Vector2 position, float speed, float health, float damage)
    {
        Position = position;
        Speed = speed;
        Health = health;
        Damage = damage;
    }
}